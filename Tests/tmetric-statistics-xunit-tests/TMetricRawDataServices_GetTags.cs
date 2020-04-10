using Xunit;
using tmetricstatistics.Services;
using System.Threading.Tasks;
using tmetricstatistics.Model;
using System.Collections.Generic;

namespace tmetric_statistics_xunit_tests
{
    public class TMetricRawDataServices_GetTags
    {
        private readonly ITMetricRawDataServices _service;
        private readonly ConfigurationFixture _configurationFixture;

        public TMetricRawDataServices_GetTags()
        {   
            this._configurationFixture = new ConfigurationFixture();
            _service = new TMetricRawDataServices(_configurationFixture.httpClientFactory);
        }

        [Fact]
        public async Task Test_TMetricRawDataServices_GetTagByName()
        {
            int.TryParse(_configurationFixture.Configuration["TMetricAccountId_PRIVATE"], out int accountId);
            int.TryParse(_configurationFixture.Configuration["TMetricUserProfileId"], out int userProfileId);
            Tag holiday_tag = await _service.GetTagByName(accountId, userProfileId, "holiday");
            
            Assert.NotNull(holiday_tag);
            Assert.True(holiday_tag.tagName.Equals("holiday"));
            Assert.True(holiday_tag.tagId > 0);
        }

    }
}
