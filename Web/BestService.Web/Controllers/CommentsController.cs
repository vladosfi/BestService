﻿namespace BestService.Web.Controllers
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

            var parrentId = input.ParentId == 0 ? (int?)null : input.ParentId;

            if (parrentId.HasValue)
            {
                if (!this.commentsService.IsInCompanyId(parrentId.Value, input.CompanyId))
                {
                    return this.BadRequest();
                }
            }

            var userId = this.userManager.GetUserId(this.User);
            var commentId = await this.commentsService.CreateAsync(input.Content, userId, input.CompanyId, input.Rating, parrentId);
            return this.RedirectToAction("Details", "Companies", new { id = input.CompanyId });
        }
    }
}
