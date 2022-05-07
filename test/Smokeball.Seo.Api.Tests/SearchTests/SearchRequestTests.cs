using Moq;
using Moq.Protected;
using Smokeball.Seo.Api.Search;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sympli.Seo.Tests.SearchTests
{
    public class SearchRequestTests
    {
        [Fact]
        public async Task When_Searching_Shoud_Return_Result()
        {
            var messageHandler = new Mock<HttpMessageHandler>();
            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync", 
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var httpClient = new HttpClient(messageHandler.Object);

            var engine = new Mock<ISearchEngine>();

            engine
                .Setup(x => x.GetUri(It.IsAny<string>()))
                .Returns(new System.Uri("https://example.com"));

            var searchResult = new SearchResult(new List<SearchResultItem>()
            {
                new SearchResultItem(1, "http://test.com")
            });

            engine.Setup(e => e.PraseContent(It.IsAny<string>()))
                .Returns(searchResult);


            var request = new SearchRequest(httpClient, _ => engine.Object);

            var result = await request.Search(new ISearchRequest.SearchParams("test", "test", CancellationToken.None));

            Assert.Equal(searchResult, result);
            engine
                .Verify(x => x.GetUri(It.IsAny<string>()));

            messageHandler.Verify();
        }
    }
}
