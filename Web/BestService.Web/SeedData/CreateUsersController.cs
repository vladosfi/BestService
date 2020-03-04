namespace BestService.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CreateUsersController : Controller
    {
        private const string UserName = "Admin";
        private const string Email = "sfi@abv.bg";
        private const string Password = "123";

        private readonly UserManager<IdentityUser> userManager;

        public CreateUsersController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> CreateUser()
        {
            var result = await this.userManager.CreateAsync(
                new IdentityUser()
                {
                    UserName = UserName,
                    Email = Email,
                    EmailConfirmed = true,
                }, Password);

            if (!result.Succeeded)
            {
                return this.BadRequest(string.Join(",", result.Errors.Select(x => x.Description)));
            }

            return this.Ok();
        }
    }
}