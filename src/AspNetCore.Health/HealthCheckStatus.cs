using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AspNetCore.Health
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HealthCheckStatus
    {
        Healthy,
        Unhealthy,
        Warning
    }
}
