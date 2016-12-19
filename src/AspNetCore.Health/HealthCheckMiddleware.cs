using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AspNetCore.Health
{
    public class HealthCheckMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHealthCheckService healthCheckService;
        private readonly PathString endPoint;

        public HealthCheckMiddleware(
            RequestDelegate next,  
            IHealthCheckService healthCheckService,
            PathString endPoint)
        {
            this.next = next;
            this.healthCheckService = healthCheckService;
            this.endPoint = endPoint;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path != endPoint)
            {
                await next.Invoke(httpContext);
            }
            else
            {
                var healthy = await healthCheckService.CheckAsync();

                var code = healthy ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                var json = JsonConvert.SerializeObject(healthCheckService.Results);

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