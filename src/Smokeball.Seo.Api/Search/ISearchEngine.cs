namespace Smokeball.Seo.Api.Search
{
    public interface ISearchEngine
    {
        Uri GetUri(string keywords);

        SearchResult PraseContent(string content);

    }
}