namespace Smokeball.Seo.Api.Search
{
    public sealed class SearchRequest : ISearchRequest
    {
        private readonly HttpClient httpClient;
        private readonly EngineMapper searchEngine;

        public SearchRequest(HttpClient httpClient, EngineMapper searchEngine)
        {
            this.httpClient = httpClient;
            this.searchEngine = searchEngine;
        }

        public async Task<SearchResult> Search(ISearchRequest.SearchParams request)
        {
            var engine = searchEngine(request.Engine);
            var uri = engine.GetUri(request.Keywords);
            var result = await httpClient.GetStringAsync(uri, request.CancellationToken);
            return engine.PraseContent(result);
        }
    }
}