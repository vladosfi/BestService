namespace BestService.Web.Controllers
{
    using System.Threading.Tasks;

    using BestService.Data.Models;
    using BestService.Services.Data;
    using BestService.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICommentsService commentsService;

        public CommentsController(
            UserManager<ApplicationUser> userManager,
            ICommentsService commentsService)
        {
            this.userManager = userManager;
            this.commentsService = commentsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommentCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var commentId = await this.commentsService.CreateAsync(input.Content, user.Id, input.CompanyId, input.Rating);
            return this.RedirectToAction(nameof(this.ById), new { id = commentId });
        }

        public IActionResult ById(int id)
        {
            return this.View();
        }
    }
}