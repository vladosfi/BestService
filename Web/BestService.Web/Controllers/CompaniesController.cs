namespace BestService.Web.Controllers
{
    using System.Threading.Tasks;

    using BestService.Data.Models;
    using BestService.Services.Data;
    using BestService.Web.ViewModels.Companies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CompaniesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompaniesService companiesService;
        private readonly ICategoriesService categoriesService;

        public CompaniesController(
            UserManager<ApplicationUser> userManager,
            ICompaniesService companiesService,
            ICategoriesService categoriesService)
        {
            this.userManager = userManager;
            this.companiesService = companiesService;
            this.categoriesService = categoriesService;
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

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            var categories = this.categoriesService.GetAll<CategoryDropdownViewModel>();
            var viewModel = new AddCompanyInputModel
            {
                Categories = categories,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddCompanyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = await this.companiesService.AddAsync(input.Name, input.Description, input.Image, input.OfficialSite, user);
            return this.RedirectToAction(nameof(this.Details), new { id = companyId });
        }
    }
}
