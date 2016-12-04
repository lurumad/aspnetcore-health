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
    }
}