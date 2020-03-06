namespace BestService.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using BestService.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class UsersSeeder : ISeeder
    {
        private const string UserName = "Admin";
        private const string Email = "sfi@abv.bg";
        private const string Password = "123456789";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(UserName);

            if (user != null)
            {
                return;
            }

            var result = await userManager.CreateAsync(
                   new ApplicationUser
                   {
                       UserName = UserName,
                       Email = Email,
                   },
                   Password);
        }
    }
}
