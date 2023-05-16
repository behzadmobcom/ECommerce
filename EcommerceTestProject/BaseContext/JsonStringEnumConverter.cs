using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ECommerce.ControllersTests.BaseContext
{
    public class JsonStringEnumConverter : StringEnumConverter
    {
        public override object? ReadJson(
            JsonReader reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer)
        {
            object? obj = default;
            try
            {
                obj = base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (JsonSerializationException exception)
            {
                if (exception.InnerException is ArgumentException)
                {
                    obj = default;
                }
                else
                {
                    throw exception;
                }
            }
            return obj;
        }
    }
}
