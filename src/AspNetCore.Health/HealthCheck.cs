using System.Threading.Tasks;

namespace AspNetCore.Health
{
    public abstract class HealthCheck
    {
        public string Name { get; }
        public string Description { get; }

        protected HealthCheck(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract Task CheckAsync(HealthCheckContext context);

        public HealthCheckResult Healthy()
        {
            return new HealthCheckResult($"{Name} ({Description})", HealthCheckStatus.Healthy);
        }

        public HealthCheckResult Unhealthy()
        {
            return new HealthCheckResult($"{Name} ({Description})", HealthCheckStatus.Unhealthy);
        }
    }
}
