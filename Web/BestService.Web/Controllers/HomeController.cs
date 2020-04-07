namespace BestService.Web.Controllers
{
    using System.Diagnostics;

    using BestService.Services.Data;
    using BestService.Web.ViewModels;
    using BestService.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public HomeController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Categories = this.categoriesService.GetAll<IndexCategoryViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult HttpError(string statusCode)
        {
            var viewModel = new CustomErrorViewModel
            {
                First = statusCode[0],
                Second = statusCode[1],
                Third = statusCode[2],
            };

            return this.View(viewModel);
        }
    }
}
