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
        public string projectName { get; set; }
        public Details details { get; set; }
        public List<int> tagsIdentifiers { get; set; }
        public List<Tag> Tags { get; set; }

        public TimeEntry()
        {
            Tags = new List<Tag>();
        }
    }
}
