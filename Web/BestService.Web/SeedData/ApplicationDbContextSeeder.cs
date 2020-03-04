namespace BestService.Web.SeedData
{
    using System;
    using System.Threading.Tasks;

    using BestService.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class ApplicationDbContextSeeder
    {
        public async Task SeedDataAsync(IServiceProvider serviceProvider, ApplicationDbContext dbContext)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
        }
    }
}
