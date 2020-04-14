using Xunit;
using tmetricstatistics.Services;
using System.Threading.Tasks;
using tmetricstatistics.Model;
using System.Collections.Generic;

namespace tmetric_statistics_xunit_tests
{
    public class TMetricRawDataServices_GetTimeEntries
    {
        private readonly ITMetricRawDataServices _service;
        private readonly ConfigurationFixture _configurationFixture;

        public TMetricRawDataServices_GetTimeEntries()
        {   
            this._configurationFixture = new ConfigurationFixture();
            _service = new TMetricRawDataServices(_configurationFixture.httpClientFactory);
        }

        [Fact]
        public async Task Test_TMetricRawDataServices_GetTimeEntries()
        {
            int.TryParse(_configurationFixture.Configuration["TMetric:AccountId"], out int accountId);
            int.TryParse(_configurationFixture.Configuration["TMetric:UserProfileId"], out int userProfileId);
            string timeRangeStartTime = "2020-03-01";
            string timeRangeEndTime = "2020-04-12";
            List<TimeEntry> timeEntries = await _service.GetTimeEntries(accountId, userProfileId, timeRangeStartTime, timeRangeEndTime);
            
            Assert.True(timeEntries.Count > 0);
        }

    }
}
