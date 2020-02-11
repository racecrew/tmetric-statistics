using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using tmetricstatistics.Model;
using System.Text.Json;

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

        public async Task<List<Project>> GetAllProjectsAsync(int accountId)
        {
            var response = await GetHttpResponseMessage(HttpMethod.Get, "api/accounts/" + accountId + "/projects");
            List<Project> _projects = null;

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                _projects = await JsonSerializer.DeserializeAsync<List<Project>>(responseStream);
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
                _accounts = await JsonSerializer.DeserializeAsync<List<Account>>(responseStream);
            }
            return _accounts;
        }
    }
}
