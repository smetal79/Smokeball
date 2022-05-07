using Smokeball.Seo.Api.Search;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Sympli.Seo.Tests.SearchTests
{
    public class SearchBehaviorTests
    {
        [Fact]
        public void When_Filtering_Matching_Url()
        {
            const string url = "https://example.com/";
            var result = CreateResultWithTwoMatchingUrl(url);

            var filter = result.FilterMatchingUrl(url);

            Assert.Equal(2, filter.Count());
        }

        [Fact]
        public void When_Result_Is_Empty_Ranking_As_String_Should_Return_Default()
        {
            var result = new SearchResult(Enumerable.Empty<SearchResultItem>());
            var ranking = result.ToRankingAsString();

            Assert.Equal("0", ranking);
        }

        [Fact]
        public void When_Result_Is_Not_Empty_Ranking_As_String_Should_Return_Rankings()
        {
            var result = new SearchResult(new List<SearchResultItem>
            {
                new SearchResultItem(1, "https://example.com"),
                new SearchResultItem(11, "https://google.com"),
                new SearchResultItem(100, "https://example.com"),
            });
            var ranking = result.ToRankingAsString();

            Assert.Equal("1,11,100", ranking);
        }

        private static SearchResult CreateResultWithTwoMatchingUrl(string url)
            => new(new List<SearchResultItem>
            {
                new SearchResultItem(1, $"{url}/1"),
                new SearchResultItem(2,$"{url}/2"),
                new SearchResultItem(3, "https://test.com/1"),
                new SearchResultItem(4, "https://gooo.com/1"),
            });
    }
}
