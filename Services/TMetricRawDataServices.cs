using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using tmetricstatistics.Model;
using System.Text.Json;
using Newtonsoft.Json;
using System.Globalization;

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

        public async Task<List<Project>> GetAllProjectsAsync(int accountId)
        {
            var response = await GetHttpResponseMessage(HttpMethod.Get, "api/accounts/" + accountId + "/projects");
            List<Project> _projects = null;

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                _projects = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Project>>(responseStream);
            }
            return _projects;
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            var response = await GetHttpResponseMessage(HttpMethod.Get, "api/userprofile/accounts");
            List<Account> _accounts = null;

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                _accounts = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Account>>(responseStream);
            }
            return _accounts;
        }

        public async Task<CalendarWeekData> GetCalendarWeekDataAsync(int accountId, int userProfileId, string startOfCalendarWeek, string endOfCalendarWeek)
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
                dynamic json_result_dyn = Newtonsoft.Json.JsonConvert.DeserializeObject(json_result);

                timeEntries = new List<TimeEntry>();
                foreach (var item in json_result_dyn)
                {
                    TimeEntry timeEntry = new TimeEntry();
                    timeEntry.startTime = item.startTime;
                    timeEntry.endTime = item.endTime;
                    timeEntry.projectId = item.details.projectId;
                    timeEntry.description = item.details.description;
                    timeEntry.projectName = item.projectName;
                    timeEntry.timeEntryId = item.timeEntryId;

                    timeEntries.Add(timeEntry);
                }
            }
            return timeEntries;
        }

    }
}
