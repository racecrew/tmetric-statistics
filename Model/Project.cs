using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tmetricstatistics.Model
{
    public class Project
    {
        public int projectId { get; set; }
        public String projectName { get; set; }
        public int projectStatus { get; set; }

        public int clientId { get; set; }

        public int accountId { get; set; }
    }
}
