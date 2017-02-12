using System.Text;
using AspNetCore.Health.Internal;

namespace AspNetCore.Health
{
    public static class HealthCheckContextExtensions
    {
        public static HealthCheckContext AddUrlCheck(this HealthCheckContext checkContext, string url)
        {
            checkContext.Add(url, async () =>
            {
                try
                {
                    var response = await HttpClientSingleton.Instance.GetAsync(url).ConfigureAwait(false);

                    return response.IsSuccessStatusCode
                        ? HealthCheckResult.Healthy($"{url}")
                        : HealthCheckResult.Unhealthy($"{url}");
                }
                catch
                {
                    return HealthCheckResult.Unhealthy($"{url}");
                }
            });

            return checkContext;
        }

        public static HealthCheckContext AddUrlChecks(this HealthCheckContext checkContext, string[] urls, string group)
        {
            checkContext.Add(group, async () =>
            {
                var successfulChecks = 0;
                var description = new StringBuilder();

                foreach (var url in urls)
                {
                    try
                    {
                        var response = await HttpClientSingleton.Instance.GetAsync(url).ConfigureAwait(false);

                        if (response.IsSuccessStatusCode)
                        {
                            successfulChecks++;
                            description.Append($"{url} OK");
                        }
                        else
                        {
                            description.Append($"{url} ERROR");
                        }
                    }
                    catch
                    {
                        description.Append($"{url} ERROR");
                    }
                }

                if (successfulChecks == urls.Length)
                {
                    return HealthCheckResult.Healthy(description.ToString());
                }

                if (successfulChecks > 0)
                {
                    return HealthCheckResult.Warning(description.ToString());
                }

                return HealthCheckResult.Unhealthy(description.ToString());
            });

            return checkContext;
        }
    }
}
