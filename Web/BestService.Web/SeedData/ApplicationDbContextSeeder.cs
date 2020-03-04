namespace BestService.Web.SeedData
{
    using System;
    using System.Threading.Tasks;

    using BestService.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class ApplicationDbContextSeeder
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext dbContext;

        private const string UserName = "Admin";
        private const string Email = "sfi@abv.bg";
        private const string Password = "123";

        public ApplicationDbContextSeeder(IServiceProvider serviceProvider, ApplicationDbContext dbContext)
        {
            this.userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            this.roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            this.dbContext = dbContext;
        }

        public async Task SeedDataAsync()
        {
            await SeedUsersAsync();
            await SeedRolesAsync();
            await SeedUserToRolesAsync();
        }

        private async Task SeedUserToRolesAsync()
        {
            throw new NotImplementedException();
        }

        private async Task SeedRolesAsync()
        {
            throw new NotImplementedException();
        }

        private async Task SeedUsersAsync()
        {
            var user = await this.userManager.FindByNameAsync(UserName);

            if (user != null)
            {
                return;
            }

            await this.userManager.CreateAsync(
                new IdentityUser
                {
                    UserName = UserName,
                    Email = Email,
                },
                Password);
        }
    }
}
