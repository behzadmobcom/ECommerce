using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace ECommerce.ControllersTests.BaseContext
{
    public static class JsonContentSerializer
    {
        public static StringContent SerializeToStringContent(object obj)
        {
            return new StringContent(
                Serialize(obj, s => s.NullValueHandling = NullValueHandling.Include),
                Encoding.Default,
                MediaTypeNames.Application.Json);
        }

        public static string Serialize(object obj, Action<JsonSerializerSettings> overrideSettingsAction = null)
        {
            JsonSerializerSettings settings = SerializerSettings.GetSerializerSettings();
            if (overrideSettingsAction != default)
            {
                overrideSettingsAction(settings);
            }
            string jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
            return jsonString;
        }

        public static async Task<T> DeserializeAsync<T>(HttpContent content)
        {
            string response = await content.ReadAsStringAsync();
            return Deserialize<T>(response);
        }

        public static T Deserialize<T>(string jsonString)
        {
            return Deserialize<T>(jsonString, typeof(T));
        }

        public static T Deserialize<T>(string jsonString, Type concreteType)
        {
            JsonSerializerSettings settings = SerializerSettings.GetSerializerSettings();
            return (T)JsonConvert.DeserializeObject(jsonString, concreteType, settings);
        }
    }
}
