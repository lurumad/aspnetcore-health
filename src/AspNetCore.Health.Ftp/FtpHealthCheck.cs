using System.Threading.Tasks;
using CoreFtp;

namespace AspNetCore.Health.Checks
{
    public class FtpHealthCheck : HealthCheck
    {
        private readonly string host;
        private readonly string username;
        private readonly string password;
        private readonly int port;
        private readonly FtpTransferMode mode;
        public FtpHealthCheck(string host, string username, string password, int port, FtpTransferMode mode, string description) : base("FTP", description)
        {
            this.host = host;
            this.username = username;
            this.password = password;
            this.port = port;
            this.mode = mode;
        }

        public override async Task CheckAsync(HealthCheckContext context)
        {
            try
            {
                using (var ftpClient = new FtpClient(new FtpClientConfiguration
                {
                    Host = host,
                    Username = username,
                    Password = password,
                    Port = port,
                    Mode = (CoreFtp.Enum.FtpTransferMode)mode
                }))
                {
                    await ftpClient.LoginAsync().ConfigureAwait(false);
                }

                context.AddResult(Healthy());
            }
            catch
            {
                context.AddResult(Unhealthy());
            }
        }
    }
}
