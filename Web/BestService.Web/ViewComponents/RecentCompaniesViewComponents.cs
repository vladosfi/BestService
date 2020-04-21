namespace BestService.Web.ViewComponents
{
    using BestService.Services.Data;
    using BestService.Web.ViewModels.RecentCompanies;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "RecentCompanies")]
    public class RecentCompaniesViewComponents : ViewComponent
    {
        private readonly ICompaniesService companiesService;

        public RecentCompaniesViewComponents(ICompaniesService companiesService)
        {
            this.companiesService = companiesService;
        }

        public IViewComponentResult Invoke(int count = 3)
        {
            var viewModel = new RecentCompaniesViewModel
            {
                Companies = this.companiesService.GetRecentlyAdded<RecentCompanyViewModel>(count),
            };

            return this.View(viewModel);
        }
    }
}
