using System;
using System.Threading.Tasks;

namespace AspNetCore.Health.Checks
{
    public class CustomHealthCheck : HealthCheck
    {
        private readonly Func<Task<bool>> check;

        public CustomHealthCheck(string serviceName, Func<Task<bool>> check) : base("Custom", serviceName)
        {
            this.check = check;
        }

        public override async Task CheckAsync(HealthCheckContext context)
        {
            try
            {
                context.AddResult(await check().ConfigureAwait(false) ? Healthy() : Unhealthy());
            }
            catch
            {
                context.AddResult(Unhealthy());
            }
        }
    }
}