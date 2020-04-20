namespace BestService.Web.Controllers
{
    using System.Threading.Tasks;

    using BestService.Services.Data;
    using BestService.Web.ViewModels.Subscribes;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class SubscribesController : ControllerBase
    {
        private const string SubscribeResponse = "Thank You For Subscribing!";
        private readonly ISubscribesService subscribesService;

        public SubscribesController(ISubscribesService subscribesService)
        {
            this.subscribesService = subscribesService;
        }

        [HttpPost]
        public async Task<ActionResult<SubscribeResponseModel>> Subscribe(SubscribeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NoContent();
            }

            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();
            await this.subscribesService.Subscribe(input.Email, ip);

            return new SubscribeResponseModel
            {
                TextResponse = SubscribeResponse,
            };
        }
    }
}
