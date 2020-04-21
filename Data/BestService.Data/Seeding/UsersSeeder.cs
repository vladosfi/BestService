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
                    UserName = GlobalConstants.AdminEmail,
                    Email = GlobalConstants.AdminEmail,
                    EmailConfirmed = true,
                    PhoneNumber = GlobalConstants.PhoneNumber,
                }, GlobalConstants.Pass);

            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = GlobalConstants.CompanyEmail,
                    Email = GlobalConstants.CompanyEmail,
                    EmailConfirmed = true,
                    PhoneNumber = GlobalConstants.PhoneNumber,
                }, GlobalConstants.Pass);

            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = GlobalConstants.UserEmail,
                    Email = GlobalConstants.UserEmail,
                    EmailConfirmed = true,
                    PhoneNumber = GlobalConstants.PhoneNumber,
                }, GlobalConstants.Pass);

            var adminUser = this.GetUserByUserName(dbContext, GlobalConstants.AdminEmail);
            var companyUser = this.GetUserByUserName(dbContext, GlobalConstants.CompanyEmail);
            var userUser = this.GetUserByUserName(dbContext, GlobalConstants.UserEmail);

            await this.AddRolesToUsers(userManager, adminUser, GlobalConstants.AdministratorRoleName);
            await this.AddRolesToUsers(userManager, companyUser, GlobalConstants.CompanyRoleName);
            await this.AddRolesToUsers(userManager, userUser, GlobalConstants.UserRoleName);
        }

        private async Task AddRolesToUsers(
            UserManager<ApplicationUser> userManager,
            ApplicationUser userName,
            string roleName)
        {
            await userManager.AddToRoleAsync(userName, roleName);
        }

        private ApplicationUser GetUserByUserName(ApplicationDbContext dbContext, string userName)
        {
            return dbContext.Users.Where(x => x.UserName == userName).FirstOrDefault();
        }
    }
}
