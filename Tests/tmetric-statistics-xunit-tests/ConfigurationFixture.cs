using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace tmetric_statistics_xunit_tests
{
    public class ConfigurationFixture : IDisposable
    {
        public IConfiguration Configuration { get; }
        public IServiceCollection services { get; }
        public ServiceProvider provider { get; private set; }

        public IHttpClientFactory httpClientFactory { get; set; }

        public ConfigurationFixture()
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<ConfigurationFixture>();
            Configuration = builder.Build();

            services = new ServiceCollection();

            string BaseUri = "https://app.tmetric.com";
      
            services.AddHttpClient("tmetricrawdata", c =>
            {
                c.BaseAddress = new Uri(BaseUri);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.DefaultRequestHeaders.Add("User-Agent", "tmetric-statistics");
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Configuration["TMetricBearerToken"]);
            });

            provider = services.BuildServiceProvider();

            httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();

        }
        public void Dispose()
        {
            // empty
        }
    }
}
