using Microsoft.AspNetCore.Mvc;
using Smokeball.Seo.Api.Constants;
using Smokeball.Seo.Api.Handlers;
using Smokeball.Seo.Api.Requests;
using System.ComponentModel.DataAnnotations;

namespace Smokeball.Seo.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchRequestHandler searchRequestHandler;

        public SearchController(ISearchRequestHandler searchRequestHandler)
        {
            this.searchRequestHandler = searchRequestHandler;
        }

        [HttpGet(Name = "GetRankings")]
        [ResponseCache(CacheProfileName = Cache.HourlyCache, VaryByQueryKeys = new[] { "keywords", "engine", "url" })]
        public async Task<IActionResult> Get(
            [Required] string keywords,
            [Required] string url,
            [RegularExpression("google|bing")] string engine = "google")
        {
            var result = await searchRequestHandler.Handle(new SearchRequest(keywords, url, engine), CancellationToken.None);
            return Ok(result);
        }
    }
}