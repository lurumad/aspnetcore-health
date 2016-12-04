using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AspNetCore.Health.Checks
{
    public class SqlServerHealthCheck : HealthCheck
    {
        private readonly string connectionString;

        public SqlServerHealthCheck(string connectionString, string description) : base("Sql Server", description)
        {
            this.connectionString = connectionString;
        }

        public override async Task CheckAsync(HealthCheckContext context)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);
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
