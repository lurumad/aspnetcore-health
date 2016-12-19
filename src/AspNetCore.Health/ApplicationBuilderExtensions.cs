using System;
using AspNetCore.Health;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHealthCheck(
            this IApplicationBuilder app,
            string endPoint)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var path = new PathString(endPoint);
            app.UseMiddleware<HealthCheckMiddleware>(path);

            return app;
        }
    }
}
