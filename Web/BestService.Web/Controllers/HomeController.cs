namespace BestService.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using BestService.Services.Mapping;
    using BestService.Web.ViewModels;
    using BestService.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public HomeController(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            var categories = this.categoriesRepository.All()
                .To<IndexCategoryViewModel>()
                .ToList();

            viewModel.Categories = categories;

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
    }
}
