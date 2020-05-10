namespace BestService.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BestService.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        /// <summary>
        /// Request body: {"term": "test"}.
        /// </summary>
        /// <returns>List of company names.</returns>
        [Produces("application/json")]
        [HttpGet("autocomplete")]
        public async Task<IActionResult> Autocomplete()
        {
            try
            {
                string term = this.HttpContext.Request.Query["term"].ToString();
                var companyNames = await this.searchService.ByStringInNameAsync(term);

                return this.Ok(companyNames);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
