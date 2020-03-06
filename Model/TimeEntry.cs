using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tmetricstatistics.Model
{
    public class TimeEntry
    {
        public int timeEntryId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string description { get; set; }
        public int projectId { get; set; }
        public string projectName { get; set; }
    }
}
