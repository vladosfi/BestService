namespace BestService.Web.Controllers
{
    using System.Threading.Tasks;

    using BestService.Common;
    using BestService.Data.Models;
    using BestService.Services.CloudinaryHelper;
    using BestService.Services.Data;
    using BestService.Web.ViewModels.Companies;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CompaniesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompaniesService companiesService;
        private readonly ICategoriesService categoriesService;
        private readonly Cloudinary cloudinary;

        public CompaniesController(
            UserManager<ApplicationUser> userManager,
            ICompaniesService companiesService,
            ICategoriesService categoriesService,
            Cloudinary cloudinary)
        {
            this.userManager = userManager;
            this.companiesService = companiesService;
            this.categoriesService = categoriesService;
            this.cloudinary = cloudinary;
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.companiesService.GetById<CompanyDetailsViewModel>(id);
            return this.View(viewModel);
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

            var transformation = new Transformation()
                .Height(GlobalConstants.ImageHeight)
                .Crop(GlobalConstants.CropImageScale);

            var imageUri = await CloudinaryExtension.UploadAsync(this.cloudinary, input.LogoImg, transformation);
            var imagePath = imageUri.Replace(GlobalConstants.CloudinaryUploadDir, string.Empty);

            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = await this.companiesService.AddAsync(
                input.Name,
                input.Description,
                imagePath,
                input.OfficialSite,
                user.Id,
                input.CategoryId);

            return this.RedirectToAction(nameof(this.Details), new { id = companyId });
        }
    }
}
