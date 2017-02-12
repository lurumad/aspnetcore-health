using System;
using System.Threading.Tasks;
using AspNetCore.Health.Checks;

namespace AspNetCore.Health
{
    public static class HealthCheckOptionsExtensions
    {
        public static HealthCheckOptions AddSqlServer(
            this HealthCheckOptions options,
            string connectionString,
            string description)
        {
            options.HealthChecks.Add(new SqlServerHealthCheck(connectionString, description));

            return options;
        }

        public static HealthCheckOptions AddRedis(
            this HealthCheckOptions options,
            string configuration,
            string description)
        {
            options.HealthChecks.Add(new RedisHealthCheck(configuration, description));

            return options;
        }

        public static HealthCheckOptions AddWebService(
            this HealthCheckOptions options,
            string serviceName,
            string url)
        {
            options.HealthChecks.Add(new WebServiceHealthCheck(serviceName, url));

            return options;
        }

        public static HealthCheckOptions Add(
            this HealthCheckOptions options,
            HealthCheck healthCheck)
        {
            options.HealthChecks.Add(healthCheck);

            return options;
        }

        public static HealthCheckOptions Add(
            this HealthCheckOptions options,
            string serviceName,
            Func<Task<bool>> check)
        {
            options.HealthChecks.Add(new CustomHealthCheck(serviceName, check));

            return options;
        }
    }
}
