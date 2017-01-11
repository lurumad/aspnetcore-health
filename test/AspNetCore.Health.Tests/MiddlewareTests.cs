using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace AspNetCore.Health.Tests
{
    public class middleware_it_should
    {
        [Fact(DisplayName = "returns a http statuscode internalservererror (500) if there are any unhealthy service")]
        public async Task returns_a_httpstatuscode_internalservererror_if_there_are_any_unhealthy_service()
        {
            var options = new HealthCheckOptions()
                .AddSqlServer("invalid connection string", "Sql Server 2012");
            var builder = new WebHostBuilder()
                .Configure(app => app.UseHealthCheck(options));
            var server = new TestServer(builder);

            var response = await server.CreateClient().GetAsync("/health");

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact(DisplayName = "returns a http statuscode ok (200) if there are no unhealthy services")]
        public async Task returns_a_httpstatuscode_ok_if_there_are_no_unhealthy_service()
        {
            var options = new HealthCheckOptions()
                .AddWebService("Google", "http://www.google.com")
                .AddFtp("ftp.uconn.edu", "anonymous", "", 21, FtpTransferMode.Binary, "Public Ftp Test");

            var builder = new WebHostBuilder()
                .Configure(app => app.UseHealthCheck(options));

            var server = new TestServer(builder);

            var response = await server.CreateClient().GetAsync("/health");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
