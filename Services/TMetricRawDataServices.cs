using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using tmetricstatistics.Model;
using System.Text.Json;
using System.Globalization;
using System.Linq;

namespace tmetricstatistics.Services
{
    public class TMetricRawDataServices : ITMetricRawDataServices
    {
        private const String c_httpClientName = "tmetricrawdata";
        private readonly IHttpClientFactory httpClientFactory;

        private async Task<HttpResponseMessage> GetHttpResponseMessage(HttpMethod httpMethod, string uri)
        {
            var request = new HttpRequestMessage(httpMethod, uri);
            var client = httpClientFactory.CreateClient(c_httpClientName);
            return await client.SendAsync(request);
        }

        public TMetricRawDataServices(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public TMetricRawDataServices()
        {
        }

        public async Task<List<Project>> GetProjects(int accountId)
        {
            var response = await GetHttpResponseMessage(HttpMethod.Get, "api/accounts/" + accountId + "/projects");
            List<Project> projects = null;

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                projects = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Project>>(responseStream);
            }
            return projects;
        }

        public async Task<Project> GetProjectByName(int accountId, string projectName)
        {
            List<Project> projects = await GetProjects(accountId);
            Project returnProject = null;
            foreach (Project project in projects)
            {
                if (project.projectName.Equals(projectName))
                {
                    returnProject = project;
                    break;
                }
            }
            return returnProject;
        }

        public async Task<List<Account>> GetAccounts()
        {
            var response = await GetHttpResponseMessage(HttpMethod.Get, "api/userprofile/accounts");
            List<Account> accounts = null;

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                accounts = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Account>>(responseStream);
            }
            return accounts;
        }

        public async Task<CalendarWeekData> GetCalendarWeekData(int accountId, int userProfileId, string startOfCalendarWeek, string endOfCalendarWeek)
        {
            var response = await GetHttpResponseMessage(HttpMethod.Get, "api/accounts/" + accountId + "/timeentries/" + userProfileId + "?timeRange.startTime=" + startOfCalendarWeek + "&timeRange.endTime=" + endOfCalendarWeek);
            CalendarWeekData calendarWeekData = null;

            if (response.IsSuccessStatusCode)
            {
                string json_result = await response.Content.ReadAsStringAsync();
                dynamic json_result_dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(json_result);
                
                calendarWeekData = new CalendarWeekData();
                calendarWeekData.actualHours = 0;
                foreach (var item in json_result_dyn)
                {
                    string startTimeAsString = item.startTime;
                    string endTimeAsString = item.endTime;
                    DateTime startTime = DateTime.ParseExact(startTimeAsString.Trim(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    DateTime endTime = DateTime.ParseExact(endTimeAsString.Trim(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    TimeSpan ts = endTime.Subtract(startTime);
                    calendarWeekData.actualHours = calendarWeekData.actualHours + ts.TotalHours;
                }
                calendarWeekData.actualHours = Math.Round(calendarWeekData.actualHours, 2);
                calendarWeekData.plannedHours = 40;
                calendarWeekData.overtime = Math.Round(calendarWeekData.actualHours - calendarWeekData.plannedHours, 2);
                calendarWeekData.startOfCalendarWeek = startOfCalendarWeek;
                calendarWeekData.endOfCalendarWeek = endOfCalendarWeek;
            }
            return calendarWeekData;
        }

        public async Task<List<TimeEntry>> GetTimeEntries(int accountId, int userProfileId, string timeRangeStartTime, string timeRangeEndTime)
        {
            var response = await GetHttpResponseMessage(HttpMethod.Get, "api/accounts/" + accountId + "/timeentries/" + userProfileId + "?timeRange.startTime=" + timeRangeStartTime + "&timeRange.endTime=" + timeRangeEndTime);
            List<TimeEntry> timeEntries = null;

            if (response.IsSuccessStatusCode)
            {
                string json_result = await response.Content.ReadAsStringAsync();
                timeEntries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TimeEntry>>(json_result);

                bool hasAtLeastOneTagIdentifier = false;
                if (timeEntries != null)
                {
                    foreach (TimeEntry timeEntry in timeEntries)
                    {
                        if (timeEntry.tagsIdentifiers.Count() > 0)
                        {
                            hasAtLeastOneTagIdentifier = true;
                            break;
                        }
                    }
                }

                Dictionary<int, Tag> tagsDict = null; // <name, Tag>
                if (hasAtLeastOneTagIdentifier)
                {
                    List<Tag> tags = await GetTags(accountId, userProfileId);
                    tagsDict = tags.ToDictionary(key => key.tagId, element => element);
                }

                foreach (TimeEntry timeEntry in timeEntries)
                {
                    foreach (int tagIdentifier in timeEntry.tagsIdentifiers)
                    {
                        timeEntry.Tags.Add(tagsDict[tagIdentifier]);
                    }
                }
            }
            return timeEntries;
        }

        public void CreateTimeEntries(int accountId, int userProfileId, List<TimeEntry> timeEntries)
        {
            string json = JsonSerializer.Serialize(timeEntries);
        }

        public async Task<List<Tag>> GetTags(int accountId, int userProfileId)
        {
            var response = await GetHttpResponseMessage(HttpMethod.Get, "api/accounts/" + accountId + "/tags");
            List<Tag> tags = null;

            if (response.IsSuccessStatusCode)
            {
                string json_result = await response.Content.ReadAsStringAsync();
                tags = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tag>>(json_result);
            }

            return tags;
        }

        public async Task<Tag> GetTagByName(int accountId, int userProfileId, string tagName)
        {
            List<Tag> tags = await GetTags(accountId, userProfileId);
            Tag returnTag = null;

            if (tags != null)
            {
                foreach (Tag tag in tags)
                {
                    if (tag.tagName.Equals(tagName))
                    {
                        returnTag = tag;
                        break;
                    }
                }
            };

            return returnTag;
        }
    }
}
