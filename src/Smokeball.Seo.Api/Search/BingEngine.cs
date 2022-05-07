using Microsoft.AspNetCore.WebUtilities;
using System.Text.RegularExpressions;

namespace Smokeball.Seo.Api.Search
{
    public sealed class BingEngine : ISearchEngine
    {
        private static readonly Regex MatchUrl = new("(?<=a href=\").+?(?=\" h)");

        private static readonly Regex MatchSearchResultItem = new("<div class=\"b_title\">(.*?)</div>");

        public Uri GetUri(string keywords)
        {
            var param = new Dictionary<string, string?>
                {
                    {"q", keywords},
                    {"count", "100"},
                    {"safeSearch", "Moderate"}
                };

            return new Uri(QueryHelpers.AddQueryString("https://www.bing.com/search", param));

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