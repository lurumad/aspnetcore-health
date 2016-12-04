using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Health
{
    public class HealthCheckContext
    {
        public ILogger Logger { get; set; }

        public ConcurrentBag<HealthCheckResult> Results { get; } = new ConcurrentBag<HealthCheckResult>();

        public void AddResult(HealthCheckResult result)
        {
            Results.Add(result);
        }

        public bool IsHealthy()
        {
            return Results.All(result => result.Status == HealthCheckStatus.Healthy);
        }
    }
}