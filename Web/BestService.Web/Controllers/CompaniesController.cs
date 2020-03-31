namespace BestService.Web.Controllers
{
    using BestService.Services.Data;
    using BestService.Web.ViewModels.Companies;
    using Microsoft.AspNetCore.Mvc;

    public class CompaniesController : Controller
    {
        private readonly ICompaniesService companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            this.companiesService = companiesService;
        }

        public IActionResult Details()
        {
            return this.View();
        }

        public IActionResult GetList()
        {
            var viewModel = new CompanyViewModel
            {
                Companies = this.companiesService.GetAll<CompaniesDetailsViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Add(AddCompanyInputModel input)
        {

            return this.View();
        }
    }
}
