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
            int.TryParse(_configurationFixture.Configuration["TMetric:AccountId"], out int accountId);
            int.TryParse(_configurationFixture.Configuration["TMetric:UserProfileId"], out int userProfileId);

            List<TimeEntry> timeEntries = new List<TimeEntry>();

            TimeEntry timeEntry1 = new TimeEntry();
            timeEntry1.startTime = "2020-04-08T08:00:00";
            timeEntry1.endTime = "2020-04-08T16:00:00";
            timeEntry1.projectName = "tmetric-statistics";
            timeEntry1.details = new Details();
            timeEntry1.details.description = "1 day";
            timeEntry1.details.projectId = 0;
            timeEntry1.Tags.Add(new Tag { tagName = "holiday" });

            TimeEntry timeEntry2 = new TimeEntry();
            timeEntry2.startTime = "2020-04-09T08:00:00";
            timeEntry2.endTime = "2020-04-09T16:00:00";
            timeEntry2.projectName = "tmetric-statistics";
            timeEntry2.details = new Details();
            timeEntry2.details.description = "1 day";
            timeEntry2.details.projectId = 0;

            timeEntries.Add(timeEntry1);
            timeEntries.Add(timeEntry2);

            bool isCreated = await _service.CreateTimeEntries(accountId, userProfileId, timeEntries);

            Assert.True(isCreated);
        }
    }
}
