using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.Health
{
    public class HealthCheckContext
    {
        public Dictionary<string, Func<ValueTask<HealthCheckResult>>> Checks { get; }

        public HealthCheckContext()
        {
            Checks = new Dictionary<string, Func<ValueTask<HealthCheckResult>>>();
        }

        public HealthCheckContext Add(string name, Func<Task<HealthCheckResult>> check)
        {
            Checks.Add(name, () => new ValueTask<HealthCheckResult>(check()));

            return this;
        }
        public HealthCheckContext Add(string name, Func<HealthCheckResult> check)
        {
            Checks.Add(name, () => new ValueTask<HealthCheckResult>(check()));

            return this;
        }
    }
}