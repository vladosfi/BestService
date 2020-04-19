namespace BestService.Web.ViewComponents
{
    using BestService.Web.ViewModels.Maps;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ViewComponent(Name = "Maps")]
    public class MapsViewComponent : ViewComponent
    {
        private readonly IOptions<GoogleMapsViewModel> mapsSettings;

        public MapsViewComponent(IOptions<GoogleMapsViewModel> mapsSettings)
        {
            this.mapsSettings = mapsSettings;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new GoogleMapsViewModel
            {
                ApiKey = this.mapsSettings.Value.ApiKey,
            };

            return this.View(viewModel);
        }
    }
}
