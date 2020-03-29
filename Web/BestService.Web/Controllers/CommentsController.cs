namespace BestService.Web.Controllers
{
    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using BestService.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : Controller
    {
        private readonly IDeletableEntityRepository<Comment> commentRepo;

        public CommentsController(IDeletableEntityRepository<Comment> commentRepo)
        {
            this.commentRepo = commentRepo;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CommentCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var comment = new Comment
            {
                Content = inputModel.Content,
                Rating = inputModel.Rating,
                CompanyId = inputModel.CompanyId,
            };

            return this.View();
        }
    }
}