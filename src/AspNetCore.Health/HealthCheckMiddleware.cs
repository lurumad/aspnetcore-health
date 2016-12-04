using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AspNetCore.Health
{
    public class HealthCheckMiddleware
    {
        private readonly ILogger<HealthCheckMiddleware> logger;
        private readonly RequestDelegate next;
        private readonly HealthCheckOptions options;

        public HealthCheckMiddleware(
            RequestDelegate next,  
            ILoggerFactory loggerFactory,
            HealthCheckOptions options)
        {
            this.next = next;
            this.options = options;
            logger = loggerFactory.CreateLogger<HealthCheckMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path != this.options.EndPoint)
            {
                await next.Invoke(httpContext);
            }
            else
            {
                var tasks = new List<Task>();
                var context = new HealthCheckContext
                {
                    Logger = logger
                };

                foreach (var healthCheck in options.HealthChecks)
                {
                    tasks.Add(healthCheck.CheckAsync(context));
                }

                await Task.WhenAll(tasks);

                var code = context.IsHealthy() ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                var json = JsonConvert.SerializeObject(context.Results);

                await WriteResponseAsync(
                    httpContext,
                    json,
                    "application/json",
                    code);
            }
        }

        protected Task WriteResponseAsync(
            HttpContext context,
            string content,
            string contentType,
            HttpStatusCode code = HttpStatusCode.OK)
        {
            context.Response.Headers["Content-Type"] = new[] { contentType };
            context.Response.Headers["Cache-Control"] = new[] { "no-cache, no-store, must-revalidate" };
            context.Response.Headers["Pragma"] = new[] { "no-cache" };
            context.Response.Headers["Expires"] = new[] { "0" };
            context.Response.StatusCode = (int) code;

            return context.Response.WriteAsync(content);
        }
    }
}