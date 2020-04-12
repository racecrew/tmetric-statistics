using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tmetricstatistics.Model;

namespace tmetricstatistics.Services
{
    public interface ITMetricRawDataServices
    {
        public Task<List<Project>> GetProjects(int accountId);
        public Task<Project> GetProjectByName(int accountId, string projectName);
        public Task<List<Account>> GetAccounts();
        public Task<CalendarWeekData> GetCalendarWeekData(int accountId, int userProfileId, string startOfCalendarWeek, string endOfCalendarWeek);
        public Task<List<TimeEntry>> GetTimeEntries(int accountId, int userProfileId, string timeRangeStartTime, string timeRangeEndTime);
        public Task<bool> CreateTimeEntries(int accountId, int userProfileId, List<TimeEntry> timeEntries);
        public Task<List<Tag>> GetTags(int accountId, int userProfileId);
        public Task<Tag> GetTagByName(int accountId, int userProfileId, string tagName);
    }
}
