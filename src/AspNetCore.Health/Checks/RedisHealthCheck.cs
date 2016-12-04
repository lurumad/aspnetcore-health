using System.Threading.Tasks;
using StackExchange.Redis;

namespace AspNetCore.Health.Checks
{
    public class RedisHealthCheck : HealthCheck
    {
        private readonly string configuration;

        public RedisHealthCheck(string configuration, string description) : base("Redis", description)
        {
            this.configuration = configuration;
        }

        public override async Task CheckAsync(HealthCheckContext context)
        {
            try
            {
                await ConnectionMultiplexer.ConnectAsync(configuration).ConfigureAwait(false);

                context.AddResult(Healthy());
            }
            catch
            {
                context.AddResult(Unhealthy());
            }
        }
    }
}