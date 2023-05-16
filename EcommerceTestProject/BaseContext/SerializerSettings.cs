using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ECommerce.ControllersTests.BaseContext
{
    public static class SerializerSettings
    {
        public static JsonSerializerSettings GetSerializerSettings()
        {
            var settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>()
            };
            ApplyDefaultSettingsTo(settings);
            return settings;
        }

        public static void ApplyDefaultSettingsTo(JsonSerializerSettings settings)
        {
            settings.Converters.Add(new JsonStringEnumConverter());
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.DateParseHandling = DateParseHandling.DateTimeOffset;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
