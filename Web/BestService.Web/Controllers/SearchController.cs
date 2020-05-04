namespace BestService.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data;
    using BestService.Services.Data;
    using BestService.Web.ViewModels.Companies;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICompaniesService companiesService;

        public SearchController(ApplicationDbContext dbContext, ICompaniesService companiesService)
        {
            this.dbContext = dbContext;
            this.companiesService = companiesService;
        }

        [Produces("application/json")]
        [HttpGet("autocomplete")]
        public async Task<IActionResult> Autocomplete()
        {
            try
            {
                string term = this.HttpContext.Request.Query["term"].ToString();
                var companyName = await this.dbContext.Companies.Where(x => x.Name.Contains(term))
                    .OrderBy(x => x.Name)
                    .Select(x => x.Name).ToListAsync();

                return this.Ok(companyName);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
