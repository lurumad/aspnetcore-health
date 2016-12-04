using System.Threading.Tasks;
using AspNetCore.Health.Internal;

namespace AspNetCore.Health.Checks
{
    public class WebServiceHealthCheck : HealthCheck
    {
        private readonly string url;

        public WebServiceHealthCheck(string serviceName, string url) : base("WebService", serviceName)
        {
            this.url = url;
        }

        public override async Task CheckAsync(HealthCheckContext context)
        {
            try
            {
                var response = await HttpClientSingleton.Instance.GetAsync(url).ConfigureAwait(false);

                context.AddResult(response.IsSuccessStatusCode ? Healthy() : Unhealthy());
            }
            catch
            {
                context.AddResult(Unhealthy());
            }
        }
    }
}