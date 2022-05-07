namespace Smokeball.Seo.Api.Search
{
    public interface ISearchRequest
    {
        public record SearchParams(string Keywords, string Engine, CancellationToken CancellationToken);
        Task<SearchResult> Search(SearchParams request);
    }
}