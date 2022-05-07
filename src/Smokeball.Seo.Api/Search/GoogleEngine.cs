using Microsoft.AspNetCore.WebUtilities;
using System.Text.RegularExpressions;

namespace Smokeball.Seo.Api.Search
{
    public sealed class GoogleEngine : ISearchEngine
    {
        private static readonly Regex MatchUrl = new("(?<=url\\?q=).+?(?=\"><)");

        private static readonly Regex MatchSearchResultItem = new("<div class=\"egMi0 kCrYT\">(.*?)</div>");

        public Uri GetUri(string keywords)
        {
            var qurey = new Dictionary<string, string?>
                {
                    {"q", keywords},
                    {"num", "100"},
                    {"safe", "active"}
                };

            return new Uri(QueryHelpers.AddQueryString("https://www.google.com/search", qurey));
        }

        public SearchResult PraseContent(string content)
        => new(GetSearchItems(content));

        public static IEnumerable<SearchResultItem> GetSearchItems(string content)
           => MatchSearchResultItem
               .Matches(content)
               .Select(e => e.Value)
               .Select(GetFirstOrDefaultUrl)
               .Where(e => !string.IsNullOrWhiteSpace(e))
           .Select((a, i) => new SearchResultItem(i + 1, a));

        private static string GetFirstOrDefaultUrl(string val)
        {
            var matched = MatchUrl.Match(val).Groups;
            var firstValue = matched.Values.FirstOrDefault();
            return firstValue != null ? firstValue.Value : string.Empty;
        }
    }
}