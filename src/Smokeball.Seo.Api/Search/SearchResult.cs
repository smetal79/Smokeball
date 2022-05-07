using System.Collections;

namespace Smokeball.Seo.Api.Search
{
    public sealed class SearchResult : IEnumerable<SearchResultItem>
    {
        private readonly IEnumerable<SearchResultItem> searchResultItems;

        public SearchResult(IEnumerable<SearchResultItem> searchResultItems)
        {
            this.searchResultItems = searchResultItems;
        }

        public IEnumerator<SearchResultItem> GetEnumerator()
        {
            foreach (var e in searchResultItems)
            {
                yield return e;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}