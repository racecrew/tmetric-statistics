using Xunit;
using tmetricstatistics.Services;
using System.Threading.Tasks;
using tmetricstatistics.Model;
using System.Collections.Generic;
using System;

namespace tmetric_statistics_xunit_tests
{
    public class TMetricRawDataServices_CreateTimeEntries
    {
        private readonly ITMetricRawDataServices _service;
        private readonly ConfigurationFixture _configurationFixture;

        public TMetricRawDataServices_CreateTimeEntries()
        {   
            this._configurationFixture = new ConfigurationFixture();
            _service = new TMetricRawDataServices(_configurationFixture.httpClientFactory);
        }

        [Fact]
        public async Task Test_TMetricRawDataServices_CreateTimeEntries()
        {
            int.TryParse(_configurationFixture.Configuration["TMetricAccountId_PRIVATE"], out int accountId);
            int.TryParse(_configurationFixture.Configuration["TMetricUserProfileId"], out int userProfileId);

            List<TimeEntry> timeEntries = new List<TimeEntry>();

            TimeEntry timeEntry = new TimeEntry();
            timeEntry.startTime = "2020-04-10T08:00:00";
            timeEntry.endTime = "2020-04-10T16:00:00";
            timeEntry.projectName = "tmetric-statistics";
            timeEntry.details = new Details();
            timeEntry.details.description = "1 day";
            timeEntry.details.projectId = 0;

            _service.CreateTimeEntries(accountId, userProfileId, timeEntries);
        }
    }
}
