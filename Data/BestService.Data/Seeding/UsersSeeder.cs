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
        private const string Pass = "123456";
        private const string PhoneNumber = "0877390025";
        private const string AdminEmail = "admin@gmail.com";
        private const string CompanyEmail = "company@gmail.com";
        private const string UserEmail = "user@gmail.com";

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
                    UserName = AdminEmail,
                    Email = AdminEmail,
                    EmailConfirmed = true,
                    PhoneNumber = PhoneNumber,
                }, Pass);

            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = CompanyEmail,
                    Email = CompanyEmail,
                    EmailConfirmed = true,
                    PhoneNumber = PhoneNumber,
                }, Pass);

            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = UserEmail,
                    Email = UserEmail,
                    EmailConfirmed = true,
                    PhoneNumber = PhoneNumber,
                }, Pass);

            var adminUser = this.GetUserByUserName(dbContext, AdminEmail);
            var companyUser = this.GetUserByUserName(dbContext, CompanyEmail);
            var userUser = this.GetUserByUserName(dbContext, UserEmail);

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
