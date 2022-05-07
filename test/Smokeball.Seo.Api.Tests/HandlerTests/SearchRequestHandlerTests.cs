using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Smokeball.Seo.Api.Handlers;
using Smokeball.Seo.Api.Search;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sympli.Seo.Tests.HandlerTests
{
    public class SearchRequestHandlerTests
    {
        [Fact]
        public async Task Given_Request_Should_Return_Ranking()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var searchRequest = fixture.Freeze<Mock<ISearchRequest>>();
            const string url = "https://example.com";

            searchRequest.Setup(r => r.Search(It.IsAny<ISearchRequest.SearchParams>()))
                .ReturnsAsync(TestSearchResult(url));

            var handler = fixture.Create<SeachRequestHandler>();

            
            var request = new Smokeball.Seo.Api.Requests.SearchRequest("test", url, "google");

            var result = await handler.Handle(request, System.Threading.CancellationToken.None);

            Assert.Equal("2,4", result);
            searchRequest.Verify(r => r.Search(It.IsAny<ISearchRequest.SearchParams>()));
        }

        private static SearchResult TestSearchResult(string url)
            => new SearchResult(new List<SearchResultItem>
            {
                new SearchResultItem(1, "https://test.com.au"),
                new SearchResultItem(2, url),
                new SearchResultItem(3, "https://test.com.au"),
                new SearchResultItem(4, url),
            });
    }
}
