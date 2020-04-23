namespace BestService.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using BestService.Services.Data;
    using BestService.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly ICategoriesService categoriesService;
        private readonly ICommentsService commentsService;
        private readonly ISubscribesService subscribesService;
        private readonly ICompaniesService companiesService;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public DashboardController(
            ISettingsService settingsService,
            ICompaniesService companiesService,
            ICategoriesService categoriesService,
            ICommentsService commentsService,
            ISubscribesService subscribesService,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.settingsService = settingsService;
            this.categoriesService = categoriesService;
            this.commentsService = commentsService;
            this.subscribesService = subscribesService;
            this.companiesService = companiesService;
            this.usersRepository = usersRepository;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                SettingsCount = await this.settingsService.GetCountAsync(),
                UsersCount = await this.usersRepository.All().CountAsync(),
                CompaniesCount = await this.companiesService.GetCountAsync(),
                CategoriesCount = await this.categoriesService.GetCountAsync(),
                CommentsCount = await this.commentsService.GetCountAsync(),
                SubscribesCount = await this.subscribesService.GetCountAsync(),
            };

            return this.View(viewModel);
        }
    }
}
