using Smokeball.Seo.Api.Search;

namespace Smokeball.Seo.Api.Handlers
{
    public interface ISearchRequestHandler
    {
        Task<string> Handle(Requests.SearchRequest request, CancellationToken cancellationToken);
    }

    public sealed class SeachRequestHandler : ISearchRequestHandler
    {
        private readonly ISearchRequest seachRequest;

        public SeachRequestHandler(ISearchRequest seachRequest)
        {
            this.seachRequest = seachRequest;
        }

        public async Task<string> Handle(Requests.SearchRequest request, CancellationToken cancellation)
        {
            var result = await seachRequest.Search(new ISearchRequest.SearchParams(request.Keyword, request.Engine, cancellation));
            return result
                .FilterMatchingUrl(request.Url)
                .ToRankingAsString();
        }
    }
}