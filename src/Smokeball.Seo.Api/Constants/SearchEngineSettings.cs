namespace Smokeball.Seo.Api.Constants
{
    public sealed class SearchEngineSettings
    {
        public const string Google = "google";
        public const string Bing = "bing";

        public static IEnumerable<string> AllEngines() => new[] { Google, Bing };
    }
}