namespace BestService.Web.Controllers
{
    using System.Threading.Tasks;

    using BestService.Common;
    using BestService.Data.Models;
    using BestService.Services.CloudinaryHelper;
    using BestService.Services.Data;
    using BestService.Services.StringHelpers;
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

            if (viewModel == null)
            {
                return this.NotFound();
            }

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

            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName) && !this.User.IsInRole(GlobalConstants.CompanyRoleName))
            {
                return this.Unauthorized();
            }

            string imagePath = await this.UploadImageToCloudinaryAsync(input.LogoImg);

            var companyId = await this.companiesService.AddAsync(
                input.Name,
                input.Description,
                imagePath,
                input.OfficialSite,
                user.Id,
                input.CategoryId);

            return this.RedirectToAction(nameof(this.Details), new { id = companyId });
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.CompanyRoleName)]
        public IActionResult Edit(int id)
        {
            var categories = this.categoriesService.GetAll<CategoryDropdownViewModel>();
            var company = this.companiesService.GetById<EditCompanyViewModel>(id);

            var viewModel = new EditCompanyViewModel
            {
                UserId = company.UserId,
                Description = company.Description,
                Name = company.Name,
                CategoryId = company.CategoryId,
                OfficialSite = company.OfficialSite,
                Categories = categories,
                LogoImage = company.LogoImage,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.CompanyRoleName)]
        public async Task<IActionResult> Edit(EditCompanyViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName) && user.Id != input.UserId)
            {
                return this.Unauthorized();
            }

            string imagePath = input.LogoImage;

            if (input.LogoImageFile != null)
            {
                await this.DeleteImageFromCloudinaryAsync(imagePath);
                imagePath = await this.UploadImageToCloudinaryAsync(input.LogoImageFile);
            }

            await this.companiesService.EditById(
                input.Id,
                input.Name,
                input.Description,
                imagePath,
                input.OfficialSite,
                input.CategoryId);

            this.TempData["Edited"] = true;

            return this.RedirectToAction(nameof(this.Details), new { id = input.Id });
        }

        private async Task<string> DeleteImageFromCloudinaryAsync(string imagePath)
        {
            var cloudinaryPublicId = StringManipulations.GetNameFromUriWithoutExtension(imagePath);
            return await CloudinaryExtension.DeleteImageImageAsync(this.cloudinary, cloudinaryPublicId);
        }

        private async Task<string> UploadImageToCloudinaryAsync(IFormFile image)
        {
            var transformation = new Transformation()
                            .Height(GlobalConstants.ImageHeight)
                            .Width(GlobalConstants.ImageWidth)
                            .Crop(GlobalConstants.CropImagePad);

            var imageUri = await CloudinaryExtension.UploadImageAsync(this.cloudinary, image, transformation);
            var imagePath = imageUri.Replace(GlobalConstants.CloudinaryUploadDir, string.Empty);
            return imagePath;
        }
    }
}
