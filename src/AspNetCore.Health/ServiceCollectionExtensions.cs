using System;
using AspNetCore.Health;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthChecks(
            this IServiceCollection services,
            Action<HealthCheckContext> checks)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var healthCheck = new HealthCheckContext();

            checks.Invoke(healthCheck);

            services.AddSingleton(healthCheck);
            services.AddSingleton<IHealthCheckService, HealthCheckService>();

            return services;
        }
    }
}
