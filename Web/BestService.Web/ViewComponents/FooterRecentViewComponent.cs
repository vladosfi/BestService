namespace BestService.Web.ViewComponents
{
    using BestService.Services.Data;
    using BestService.Web.ViewModels.FooterRecent;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "FooterRecent")]
    public class FooterRecentViewComponent : ViewComponent
    {
        private readonly ICompaniesService companiesService;

        public FooterRecentViewComponent(ICompaniesService companiesService)
        {
            this.companiesService = companiesService;
        }

        public IViewComponentResult Invoke(int count = 2)
        {
            var viewModel = new FooterRecentCompaniesViewModel
            {
                 Companies = this.companiesService.GetRecentlyAdded<FooterRecentCompanyViewModel>(count),
            };

            return this.View(viewModel);
        }
    }
}
