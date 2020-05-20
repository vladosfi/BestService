namespace BestService.Web.Controllers
{
    using System;
    using System.Linq;
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
        private const int ItemsPerPage = 6;
        private const string DefaultSortOrder = "Newest";
        private const string NoResultKey = "NoResults";

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompaniesService companiesService;
        private readonly ICategoriesService categoriesService;
        private readonly Cloudinary cloudinary;
        private readonly IRatingsService ratingsService;

        public CompaniesController(
            UserManager<ApplicationUser> userManager,
            ICompaniesService companiesService,
            ICategoriesService categoriesService,
            Cloudinary cloudinary,
            IRatingsService ratingsService)
        {
            this.userManager = userManager;
            this.companiesService = companiesService;
            this.categoriesService = categoriesService;
            this.cloudinary = cloudinary;
            this.ratingsService = ratingsService;
        }

        public async Task<IActionResult> Details(int id, string companyName = null)
        {
            if (companyName != null)
            {
                var model = this.companiesService.GetByName<CompanyDetailsViewModel>(companyName);

                if (model == null)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent);
                }

                return this.View(model);
            }

            var viewModel = this.companiesService.GetById<CompanyDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            if (user != null)
            {
                viewModel.IsRateAllowed = this.ratingsService.IsRateAllowed(id, user.Id);

                var company = this.companiesService.GetById<CompanyDetailsViewModel>(id);

                if (this.User.IsInRole(GlobalConstants.AdministratorRoleName) ||
                    (this.User.IsInRole(GlobalConstants.CompanyRoleName) && user.Id == company.User.Id))
                {
                    viewModel.IsContentEditor = true;
                }
            }

            viewModel.TagCloud = this.companiesService.GetTagCloud(id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Search(int page, string sortOrder = DefaultSortOrder, int show = ItemsPerPage, string searchTerm = null, bool searchByTag = false)
        {
            page = page <= 0 ? 1 : page;

            string propertyReference = searchByTag == true ? "Title" : "Description";

            CompanyViewModel viewModel = new CompanyViewModel
            {
                SortOrder = sortOrder,
                ItemsCount = show,
                SearchTerm = searchTerm,
                Companies = await this.companiesService.SearchText<CompaniesDetailsViewModel>(propertyReference, searchTerm, show, (page - 1) * show),
            };

            int count = await this.companiesService.GetSearchCountAsync(propertyReference, searchTerm);

            viewModel.PagesCount = (int)Math.Ceiling((double)count / show);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            if (count == 0)
            {
                this.TempData[NoResultKey] = true;
                return this.View(viewModel);
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> GetList(int page, string sortOrder = DefaultSortOrder, int show = ItemsPerPage)
        {
            page = page <= 0 ? 1 : page;

            CompanyViewModel viewModel = new CompanyViewModel
            {
                SortOrder = sortOrder,
                ItemsCount = show,
                Companies = await this.companiesService.GetByPages<CompaniesDetailsViewModel>(show, (page - 1) * show, sortOrder),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            int count = await this.companiesService.GetCountAsync();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / show);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

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
        [Authorize(Roles = "Administrator, Company")]
        public async Task<IActionResult> Edit(int id)
        {
            var company = this.companiesService.GetById<EditCompanyViewModel>(id);

            if (company == null)
            {
                return this.BadRequest();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName) && user.Id != company.UserId)
            {
                return this.Unauthorized();
            }

            var categories = this.categoriesService.GetAll<CategoryDropdownViewModel>();

            var viewModel = new EditCompanyViewModel
            {
                UserId = company.UserId,
                Description = company.Description,
                Name = company.Name,
                CategoryId = company.CategoryId,
                OfficialSite = company.OfficialSite,
                LogoImage = company.LogoImage,
                Categories = categories,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Company")]
        public async Task<IActionResult> Edit(EditCompanyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var company = this.companiesService.GetById<EditCompanyInputModel>(input.Id);

            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName) && user.Id != company.UserId)
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
