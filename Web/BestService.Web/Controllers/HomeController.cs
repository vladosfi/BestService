namespace BestService.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using BestService.Common;
    using BestService.Services.Data;
    using BestService.Web.ViewModels;
    using BestService.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    public class HomeController : BaseController
    {
        private readonly ICompaniesService companiesService;
        private readonly ICommentsService commentsService;
        private readonly IRatingsService ratingsService;
        private readonly ISubscribesService subscribesService;
        private readonly ILogger<HomeController> logger;

        public HomeController(
            ICompaniesService companiesService,
            ICommentsService commentsService,
            IRatingsService ratingsService,
            ISubscribesService subscribesService,
            ILogger<HomeController> logger)
        {
            this.companiesService = companiesService;
            this.commentsService = commentsService;
            this.ratingsService = ratingsService;
            this.subscribesService = subscribesService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                CompaniesCount = await this.companiesService.GetCountAsync(),
                CommentsCount = await this.commentsService.GetCountAsync(),
                RateCount = this.ratingsService.GetAllCount(),
                Subscribers = await this.subscribesService.GetCountAsync(),
                MostRecentCompanies = this.companiesService.GetRecentlyAdded<IndexCompanyDetailsViewModel>(3),
                MostCommentedCompanies = this.companiesService.GetMostCommented<IndexCompanyDetailsViewModel>(3),
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
            this.logger.LogError($"Request ID:{Activity.Current?.Id ?? this.HttpContext.TraceIdentifier}: {Environment.StackTrace}");

            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult HttpError(int? statusCode)
        {
            var statusMessage = string.Empty;

            switch (statusCode)
            {
                case 400:
                    statusMessage = GlobalConstants.ErrorStatusBadRequest;
                    break;
                case 403:
                    statusMessage = GlobalConstants.ErrorStatusForbidden;
                    break;
                case 404:
                    statusMessage = GlobalConstants.ErrorStatusPageNotFound;
                    break;
                case 408:
                    statusMessage = GlobalConstants.ErrorStatusTimeout;
                    break;
                case 500:
                    statusMessage = GlobalConstants.ErrorStatusInternalServerError;
                    break;
                default:
                    statusMessage = GlobalConstants.ErrorStatusOther;
                    break;
            }

            string reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode.Value);

            var viewModel = new CustomErrorViewModel
            {
                StatusCodeNumber = statusCode.Value,
                ReasonPhrase = reasonPhrase,
                StatusMessage = statusMessage,
            };

            return this.View(viewModel);

        }
    }
}
