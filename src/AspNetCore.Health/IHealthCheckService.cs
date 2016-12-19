using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.Health
{
    public interface IHealthCheckService
    {
        Task<bool> CheckAsync();

        IList<HealthCheckResult> Results { get; }
    }
}
