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
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if (dbContext.Users.Any())
            {
                return;
            }

            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = SeederConstants.AdminEmail,
                    Email = SeederConstants.AdminEmail,
                    EmailConfirmed = true,
                    PhoneNumber = SeederConstants.PhoneNumber,
                }, SeederConstants.Pass);

            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = SeederConstants.CompanyEmail,
                    Email = SeederConstants.CompanyEmail,
                    EmailConfirmed = true,
                    PhoneNumber = SeederConstants.PhoneNumber,
                }, SeederConstants.Pass);

            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = SeederConstants.UserEmail,
                    Email = SeederConstants.UserEmail,
                    EmailConfirmed = true,
                    PhoneNumber = SeederConstants.PhoneNumber,
                }, SeederConstants.Pass);

            var adminUser = this.GetUserByUserName(dbContext, SeederConstants.AdminEmail);
            var companyUser = this.GetUserByUserName(dbContext, SeederConstants.CompanyEmail);
            var userUser = this.GetUserByUserName(dbContext, SeederConstants.UserEmail);

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
