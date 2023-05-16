using System.Collections;
using System.Reflection;
using System.Text;
using System.Web;

namespace ECommerce.ControllersTests.BaseContext
{
    public class QueryStringConverter : IQueryStringConverter
    {
        public string ConvertToQueryString<T>(T queryParams)
            where T : class
        {
            var queryStringBuilder = new StringBuilder();
            IEnumerable<PropertyInfo> props = typeof(T).GetProperties().Where(r => r.GetValue(queryParams) != default);
            IEnumerable<PropertyInfo> dateTimeProps = props
                .Where(p => typeof(DateTimeOffset?).IsAssignableFrom(p.PropertyType)
                    || typeof(DateTime?).IsAssignableFrom(p.PropertyType));
            IEnumerable<PropertyInfo> collectionsProps = props
                .Where(p => typeof(IEnumerable).IsAssignableFrom(p.PropertyType)
                    && !typeof(string).IsAssignableFrom(p.PropertyType));
            IEnumerable<PropertyInfo> expandProps = props.Where(p => IsFlagsEnum(p));
            props = props.Except(dateTimeProps).Except(collectionsProps).Except(expandProps);
            IEnumerable<string> queryStrings = GetPropsQueryStrings(queryParams, props);
            queryStrings = AppendDateTimePropsToQueryStringsIfAny(queryParams, dateTimeProps, queryStrings);
            queryStrings = AppendCollectionPropsToQueryStringsIfAny(queryParams, collectionsProps, queryStrings);
            queryStrings = AppendExpandPropToQueryStringsIfExists(queryParams, expandProps, queryStrings);
            queryStringBuilder.AppendJoin('&', queryStrings);
            return queryStringBuilder.ToString();
        }

        private bool IsFlagsEnum(PropertyInfo expandProp)
        {
            bool isToSearchOnInheritanceChain = false;
            return expandProp.PropertyType.IsEnum
                && expandProp.PropertyType.IsDefined(typeof(FlagsAttribute), isToSearchOnInheritanceChain);
        }

        private IEnumerable<string> GetPropsQueryStrings<T>(
            T queryParams,
            IEnumerable<PropertyInfo> props)
            where T : class
        {
            return props.Select(r => $"{HttpUtility.UrlEncode(r.Name)}=" +
                $"{HttpUtility.UrlEncode(r.GetValue(queryParams).ToString())}");
        }

        private IEnumerable<string> AppendDateTimePropsToQueryStringsIfAny<T>(
            T queryParams,
            IEnumerable<PropertyInfo> dateTimesProps,
            IEnumerable<string> queryStrings)
            where T : class
        {
            foreach (PropertyInfo dateTimeProp in dateTimesProps)
            {
                string dateTimeQueryString = $"{dateTimeProp.Name}=";
                object dateTime = dateTimeProp.GetValue(queryParams);
                dateTimeQueryString = string.Concat(dateTimeQueryString, $"{dateTime:yyyy-MM-ddTHH:mm:ss.fffZ}");
                queryStrings = queryStrings.Append(dateTimeQueryString);
            }
            return queryStrings;
        }

        private IEnumerable<string> AppendCollectionPropsToQueryStringsIfAny<T>(
            T queryParams,
            IEnumerable<PropertyInfo> collectionProps,
            IEnumerable<string> queryStrings)
            where T : class
        {
            foreach (PropertyInfo collectionProp in collectionProps)
            {
                var collection = (IList)collectionProp.GetValue(queryParams);
                if (collection.Count > 0)
                {
                    string collectionQueryString = CreateCollectionPropQueryString(collectionProp, collection);
                    queryStrings = queryStrings.Append(collectionQueryString);
                }
            }
            return queryStrings;
        }

        private string CreateCollectionPropQueryString(PropertyInfo collectionProp, IList collection)
        {
            string collectionQueryString = $"{collectionProp.Name}=";
            bool isFirstElement = true;
            foreach (object element in collection)
            {
                if (isFirstElement)
                {
                    isFirstElement = false;
                    collectionQueryString = string.Concat(collectionQueryString, element.ToString());
                }
                else
                {
                    collectionQueryString = string.Concat(collectionQueryString, $",{element}");
                }
            }
            return collectionQueryString;
        }

        private IEnumerable<string> AppendExpandPropToQueryStringsIfExists<T>(
            T queryParams,
            IEnumerable<PropertyInfo> expandProps,
            IEnumerable<string> queryStrings)
            where T : class
        {
            foreach (PropertyInfo expandProperty in expandProps)
            {
                queryStrings = queryStrings.Append(expandProperty.Name + "=" + expandProperty.GetValue(queryParams));
            }
            return queryStrings;
        }
    }
}
