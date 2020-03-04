using Xunit;
using tmetricstatistics.Services;

namespace tmetric_statistics_xunit_tests
{
    public class TMetricRawDataServices_GetTimeEntries
    {
        private readonly ITMetricRawDataServices _service;
        private readonly ConfigurationFixture _configurationFixture;

        public TMetricRawDataServices_GetTimeEntries()
        {
            _service = new TMetricRawDataServices();
            this._configurationFixture = new ConfigurationFixture();
        }

        [Fact]
        public void Test_TMetricRawDataServices_GetTimeEntries()
        {
            int.TryParse(_configurationFixture.Configuration["TMetricAccountId"], out int accountId);
            int.TryParse(_configurationFixture.Configuration["TMetricUserProfileId"], out int userProfileId);
            string timeRangeStartTime = "2020-03-01";
            string timeRangeEndTime = "2020-03-03";
            var result = _service.GetTimeEntries(accountId, userProfileId, timeRangeStartTime, timeRangeEndTime);
        }

    }
}
