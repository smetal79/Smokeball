namespace Smokeball.Seo.Api.Search
{
    public static class SearchResultBehaviors
    {
        public static SearchResult FilterMatchingUrl(this SearchResult result, string matchUrl)
            => new(result.Where(f => f.Content.Contains(matchUrl, StringComparison.OrdinalIgnoreCase)));

        public static string ToRankingAsString(this SearchResult result)
        {
            const string NoRanking = "0";
            var rankings = result.Select(f => f.Ranking).ToList();
            return rankings.Any() ? string.Join(",", rankings.Select(f => f)) : NoRanking;
        }
    }
}