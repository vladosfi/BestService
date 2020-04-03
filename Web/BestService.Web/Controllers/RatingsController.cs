namespace BestService.Web.Controllers
{
    using System.Threading.Tasks;
    using BestService.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsService ratingsService;

        public RatingsController(IRatingsService ratingsService)
        {
            this.ratingsService = ratingsService;
        }

        [HttpPost]
        public IActionResult Rate()
        {
            return this.Ok();
        }
    }
}