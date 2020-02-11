using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tmetricstatistics.Model
{
    public class TimeEntries
    {
        public int ID { get; set; }

        public String endTime { get; set; }
        public int timerDuration { get; set; }
        public String startTime { get; set; }
        public int timeEntryId { get; set; }
        public String description { get; set; }
        public String projectName { get; set; }
        public Boolean isDeleted { get; set; }

    }
}
