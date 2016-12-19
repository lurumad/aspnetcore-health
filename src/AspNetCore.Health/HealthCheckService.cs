using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Health
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly HealthCheckContext healthCheckContext;
        private readonly ILogger<HealthCheckService> logger;

        public IList<HealthCheckResult> Results { get; }

        public HealthCheckService(
            HealthCheckContext healthCheckContext,
            ILogger<HealthCheckService> logger)
        {
            this.healthCheckContext = healthCheckContext;
            this.logger = logger;
            Results = new List<HealthCheckResult>();
        }

        public async Task<bool> CheckAsync()
        {
            var healthy = true;
            var state = new StringBuilder();

            foreach (var check in healthCheckContext.Checks)
            {
                try
                {
                    var result = await check.Value();

                    Results.Add(result);

                    healthy = result.Status == HealthCheckStatus.Healthy || result.Status == HealthCheckStatus.Warning;

                    state.Append($"{check.Key} : {(healthy ? "Healthy" : "Unhealthy")}");
                }
                catch (Exception)
                {
                    healthy = false;
                }

                logger.Log(healthy ? LogLevel.Information : LogLevel.Error, 0, state, null, (builder, exception) => builder.ToString());
            }

            return healthy;
        }
    }
}