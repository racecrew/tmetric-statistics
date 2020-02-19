using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tmetricstatistics.Model
{
    public class CalendarWeekData
    {
        public int calendarWeek { get; set; }
        public String startOfCalendarWeek { get; set; }
        public String endOfCalendarWeek { get; set; }
        public double plannedHours { get; set; }
        public double actualHours { get; set; }
        public double overtime { get; set; }
        public int sickLeave { get; set; }
        public int holidays { get; set; }
        public int bridgeDay { get; set; }
        public int publicHoliday { get; set; }

    }
}
