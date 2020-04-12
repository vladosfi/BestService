namespace BestService.Web.Controllers
{
    using BestService.Services.Data;
    using BestService.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult ByName(string name, int? page = 1)
        {
            var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
