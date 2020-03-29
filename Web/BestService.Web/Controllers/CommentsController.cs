namespace BestService.Web.Controllers
{
    using System;
    using BestService.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : Controller
    {
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CommentCreateInputModel inputModel)
        {
            return this.View();
        }
    }
}