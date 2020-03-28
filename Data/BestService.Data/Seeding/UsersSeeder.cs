namespace BestService.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class UsersSeeder : ISeeder
    {
        private const string Pass = "123456";
        private const string PhoneNumber = "0877390025";
        private const string Email = "vladosfi@gmail.com";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (dbContext.Users.Any())
            {
                return;
            }

            var userResult = await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = "vlado",
                    Email = Email,
                    EmailConfirmed = true,
                    PhoneNumber = PhoneNumber,
                }, Pass);

            var adminResult = await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = "admin",
                    Email = Email,
                    EmailConfirmed = true,
                    PhoneNumber = PhoneNumber,
                }, Pass);

            var firmResult = await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = "firm",
                    Email = Email,
                    EmailConfirmed = true,
                    PhoneNumber = PhoneNumber,
                }, Pass);
        }
    }
}
