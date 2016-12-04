using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Health
{
    public class HealthCheckOptions
    {
        public PathString EndPoint { get; set; }

        public IList<HealthCheck> HealthChecks { get; } = new List<HealthCheck>();

        public HealthCheckOptions()
        {
            EndPoint = new PathString("/health");
        }
    }
}