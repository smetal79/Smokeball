using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Smokeball.Seo.Ui.Services
{
    public sealed class FetchSeoStatus : IFetchSeoStatus
    {
        private readonly HttpClient httpClient;

        public FetchSeoStatus(HttpClient httpClient)
            => this.httpClient = httpClient;

        public Task<string> GetRankings(GetStatusRequest request)
        {
            var queryString = BuildSearchQueryString(request);
            return httpClient.GetStringAsync(queryString);
        }

        private static string BuildSearchQueryString(GetStatusRequest request)
        {
            var query = new Dictionary<string, string>
            {
                { "keywords", request.Keywords },
                { "url", request.Uri },
                { "engine", "google" },
            };

            return QueryHelpers.AddQueryString(string.Empty, query);
        }
    }
}
