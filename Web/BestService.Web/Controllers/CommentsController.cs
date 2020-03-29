namespace BestService.Web.Controllers
{
    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using BestService.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CommentsController : Controller
    {
        private readonly IDeletableEntityRepository<Comment> commentRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            IDeletableEntityRepository<Comment> commentRepo,
            UserManager<ApplicationUser> userManager)
        {
            this.commentRepo = commentRepo;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommentCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var comment = new Comment
            {
                Content = inputModel.Content,
                Rating = inputModel.Rating,
                CompanyId = inputModel.CompanyId,
                UserId = user.Id,
            };

            return this.View();
        }
    }
}