namespace BestService.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BestService.Data.Models;
    using BestService.Services.Data;
    using BestService.Web.ViewModels.Ratings;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsService ratingsService;
        private readonly UserManager<ApplicationUser> userManager;

        public RatingsController(
            IRatingsService ratingsService,
            UserManager<ApplicationUser> userManager)
        {
            this.ratingsService = ratingsService;
            this.userManager = userManager;
        }

        /// <summary>
        /// Request body: {"companyId": 1}.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RateResponseModel>> Rate(RateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NoContent();
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.ratingsService.RateAsync(input.CompanyId, userId, input.Stars);
            var rateSum = await this.ratingsService.GetCompanyRates(input.CompanyId);
            var rateAvg = await this.ratingsService.GetAvgCompanyRate(input.CompanyId);

            return new RateResponseModel
            {
                CompanyId = input.CompanyId,
                RateValue = input.Stars,
                RateSum = rateSum,
                RateAvg = Math.Round(rateAvg, 1, MidpointRounding.AwayFromZero),
            };
        }
    }
}
