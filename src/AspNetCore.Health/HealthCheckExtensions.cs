using System;
using Microsoft.AspNetCore.Builder;

namespace AspNetCore.Health
{
    public static class HealthCheckExtensions
    {
        public static IApplicationBuilder UseHealthCheck(
            this IApplicationBuilder app,
            HealthCheckOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            app.UseMiddleware<HealthCheckMiddleware>(options);

            return app;
        }
    }
}
