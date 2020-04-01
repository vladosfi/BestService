namespace BestService.Web.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using BestService.Data.Models;
    using BestService.Services;
    using BestService.Services.Data;
    using BestService.Web.ViewModels.Companies;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CompaniesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompaniesService companiesService;
        private readonly ICategoriesService categoriesService;
        private readonly CloudinariesService cloudinariesService;

        public CompaniesController(
            UserManager<ApplicationUser> userManager,
            ICompaniesService companiesService,
            ICategoriesService categoriesService,
            CloudinariesService cloudinariesService)
        {
            this.userManager = userManager;
            this.companiesService = companiesService;
            this.categoriesService = categoriesService;
            this.cloudinariesService = cloudinariesService;
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

            await this.UploadImage(request)

            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = await this.companiesService.AddAsync(input.Name, input.Description, input.Image, input.OfficialSite, user, input.CategoryId);
            return this.RedirectToAction(nameof(this.Details), new { id = companyId });
        }

        private async Task<string> UploadImage(IFormFile formFile, string imageName)
        {
            var url = await this.cloudinariesService.UploadImage(
                    formFile,
                    name: $"{imageName}",
                    transformation: new Transformation().Width(300).Height(300));

            return url;
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadImage(ICollection<FormFile> files)
        //{
        //    var uploadParams = new ImageUploadParams()
        //    {
        //        File = new FileDescription(@"c:\my_image.jpg"),
        //    };

        //    var uploadResult = await this.cloudinary.UploadAsync(uploadParams);

        //    if (uploadResult.StatusCode != HttpStatusCode.OK)
        //    {
        //        return this.RedirectToAction(nameof(this.Add));
        //    }

        //    return null;
        //}
    }
}
