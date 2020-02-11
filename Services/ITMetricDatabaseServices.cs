using System.Collections.Generic;
using tmetricstatistics.Model;

namespace tmetricstatistics.Services
{
    public interface ITMetricDatabaseServices
    {
        public List<Project> GetAllProjects();
    }
}
