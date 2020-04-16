namespace BestService.Web.Controllers
{
    using System;

    using BestService.Services.Data;
    using BestService.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private const int ItemsPerPage = 6;

        private readonly ICategoriesService categoriesService;
        private readonly ICompaniesService companiesService;

        public CategoriesController(
            ICategoriesService categoriesService,
            ICompaniesService companiesService)
        {
            this.categoriesService = categoriesService;
            this.companiesService = companiesService;
        }

        public IActionResult ByName(string name, int page = 1)
        {
            var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);

            if (viewModel == null || page <= 0)
            {
                return this.NotFound();
            }

            viewModel.Companies = this.companiesService
                .GetByCategoryId<CompaniesInCategoryViewModel>(viewModel.Id, ItemsPerPage, (page - 1) * ItemsPerPage);

            int count = this.companiesService.GetCountByCategoryId(viewModel.Id);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public IActionResult All()
        {
            var viewModel = new AllCategoriesViewModel
            {
                Categories = this.categoriesService.GetAll<CategoriesViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
