using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tmetricstatistics.Model;

namespace tmetricstatistics.Services
{
    public interface ITMetricRawDataServices
    {
        public Task<List<Project>> GetAllProjectsAsync(int accountId);
        public Task<List<Account>> GetAllAccountsAsync();
        public Task<CalendarWeekData> GetCalendarWeekDataAsync(int accountId, int userProfileId, string startOfCalendarWeek, string endOfCalendarWeek);
        public Task<List<TimeEntry>> GetTimeEntries(int accountId, int userProfileId, string timeRangeStartTime, string timeRangeEndTime);
        public Task<List<Tag>> GetTags(int accountId, int userProfileId);
        public Task<Tag> GetTagByName(int accountId, int userProfileId, string tagName);
    }
}
