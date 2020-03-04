using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace tmetric_statistics_xunit_tests
{
    public class ConfigurationFixture : IDisposable
    {
        public IConfiguration Configuration { get; }
        public ConfigurationFixture()
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<ConfigurationFixture>();
            Configuration = builder.Build();
        }
        public void Dispose()
        {
            // empty
        }
    }
}
