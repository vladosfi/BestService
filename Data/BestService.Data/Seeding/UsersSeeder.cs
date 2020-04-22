namespace BestService.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Common;
    using BestService.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = GlobalConstants.AdminName,
                    Email = GlobalConstants.AdminEmail,
                    EmailConfirmed = true,
                    PhoneNumber = GlobalConstants.PhoneNumber,
                }, GlobalConstants.Pass);

            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = GlobalConstants.CompanyName,
                    Email = GlobalConstants.CompanyEmail,
                    EmailConfirmed = true,
                    PhoneNumber = GlobalConstants.PhoneNumber,
                }, GlobalConstants.Pass);

            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = GlobalConstants.UserName,
                    Email = GlobalConstants.UserEmail,
                    EmailConfirmed = true,
                    PhoneNumber = GlobalConstants.PhoneNumber,
                }, GlobalConstants.Pass);

            var adminUser = this.GetUserByUserName(dbContext, GlobalConstants.AdminName);
            var companyUser = this.GetUserByUserName(dbContext, GlobalConstants.CompanyName);
            var userUser = this.GetUserByUserName(dbContext, GlobalConstants.UserName);

            await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdministratorRoleName);
            await userManager.AddToRoleAsync(companyUser, GlobalConstants.CompanyRoleName);
            await userManager.AddToRoleAsync(userUser, GlobalConstants.UserRoleName);
        }

        private ApplicationUser GetUserByUserName(ApplicationDbContext dbContext, string userName)
        {
            return dbContext.Users.Where(x => x.UserName == userName).FirstOrDefault();
        }
    }
}
