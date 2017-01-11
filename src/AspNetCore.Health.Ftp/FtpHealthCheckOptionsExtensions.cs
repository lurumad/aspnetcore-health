using AspNetCore.Health.Checks;

namespace AspNetCore.Health
{
    public static class FtpHealthCheckOptionsExtensions
    {
        public static HealthCheckOptions AddFtp(
            this HealthCheckOptions options,
            string host, 
            string username,
            string password, 
            int port, 
            FtpTransferMode mode, 
            string description)
        {
            options.HealthChecks.Add(new FtpHealthCheck(host, username, password, port, mode, description));

            return options;
        }
    }
}
