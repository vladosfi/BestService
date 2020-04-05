namespace BestService.Web.Controllers
{
    using System.Threading.Tasks;

    using BestService.Data.Models;
    using BestService.Services.Data;
    using BestService.Web.ViewModels.Visits;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VisitsController : Controller
    {
        private readonly IVisitsService visitsService;
        private readonly UserManager<ApplicationUser> userManager;

        public VisitsController(
            IVisitsService visitsService,
            UserManager<ApplicationUser> userManager)
        {
            this.visitsService = visitsService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<VisitResponseModel>> Visit(VisitInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var visitCount = await this.visitsService.IncreaseVisit(input.CompanyId, userId);

            return new VisitResponseModel
            {
                VisitCount = visitCount + 1,
            };
        }
    }
}
