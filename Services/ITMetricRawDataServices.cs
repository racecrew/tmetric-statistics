using System.Collections.Generic;
using System.Threading.Tasks;
using tmetricstatistics.Model;

namespace tmetricstatistics.Services
{
    public interface ITMetricRawDataServices
    {
        public Task<List<Project>> GetAllProjectsAsync(int accountId);
        public Task<List<Account>> GetAllAccountsAsync();
    }
}
