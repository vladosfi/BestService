namespace BestService.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class ApplicationDbContextSeeder1
    {
        private const string UserName = "Admin";
        private const string Email = "sfi@abv.bg";
        private const string Password = "123";
        private const string RoleName = "Admin";

        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext dbContext;


        public ApplicationDbContextSeeder1(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            this.userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            this.roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            this.dbContext = dbContext;
        }

        public async Task SeedAsync()
        {
            await this.SeedUsersAsync();
            await this.SeedRolesAsync();
            await this.SeedUserToRolesAsync();
        }

        private async Task SeedUserToRolesAsync()
        {
            var user = await this.userManager.FindByNameAsync(UserName);
            var role = await this.roleManager.FindByNameAsync(RoleName);

            var exist = this.dbContext.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);

            if (exist)
            {
                return;
            }

            this.dbContext.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = user.Id,
            });

            await this.dbContext.SaveChangesAsync();
        }

        private async Task SeedRolesAsync()
        {
            var role = await this.roleManager.FindByNameAsync(RoleName);

            if (role != null)
            {
                return;
            }

            await this.roleManager.CreateAsync(new IdentityRole
            {
                Name = RoleName,
            });
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
