namespace AspNetCore.Health
{
    public class HealthCheckResult
    {
        public string Name { get; }

        public HealthCheckStatus Status { get; }

        public HealthCheckResult(string name, HealthCheckStatus status)
        {
            Name = name;
            Status = status;
        }

        public static HealthCheckResult Healthy(string description)
        {
            return new HealthCheckResult($"{description}", HealthCheckStatus.Healthy);
        }

        public static HealthCheckResult Unhealthy(string description)
        {
            return new HealthCheckResult($"{description}", HealthCheckStatus.Unhealthy);
        }

        public static HealthCheckResult Warning(string description)
        {
            return new HealthCheckResult($"{description}", HealthCheckStatus.Warning);
        }
    }
}