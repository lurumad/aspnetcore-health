using System.Net;
using System.Threading.Tasks;
using AspNetCore.Health.Internal;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AspNetCore.Health.Tests
{
    public class middleware_it_should
    {
        [Fact]
        public async Task returns_a_httpstatuscode_internalservererror_if_there_are_any_unhealthy_service()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                    {
                        services.AddHealthChecks(context =>
                        {
                            context.AddUrlCheck("http://invalidurl");
                        });
                    })
                .Configure(app => app.UseHealthCheck("/health"));
            var server = new TestServer(builder);

            var response = await server.CreateClient().GetAsync("/health");

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task returns_a_httpstatuscode_internalservererror_if_there_are_any_unhealthy_service_with_atleast_one_healthy()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                    {
                        services.AddHealthChecks(context =>
                        {
                            context.AddUrlCheck("http://invalidurl");
                            context.AddUrlCheck("http://www.google.com");
                        });
                    })
                .Configure(app => app.UseHealthCheck("/health"));
            var server = new TestServer(builder);

            var response = await server.CreateClient().GetAsync("/health");

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task returns_a_httpstatuscode_ok_if_there_are_any_unhealthy_service()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHealthChecks(context =>
                    {
                        context
                            .AddUrlCheck("http://www.google.com")
                            .AddFtp("ftp.uconn.edu", "anonymous", "", 21, FtpTransferMode.Binary, "Public Ftp Test");
                    });
                })
                .Configure(app => app.UseHealthCheck("/health"));

            var server = new TestServer(builder);

            var response = await server.CreateClient().GetAsync("/health");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
