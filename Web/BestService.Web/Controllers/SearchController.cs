namespace BestService.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public SearchController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = this.HttpContext.Request.Query["term"].ToString();
                var companyName = await this.dbContext.Companies.Where(x => x.Name.Contains(term))
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