namespace BestService.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Models;

    internal class SettingsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await dbContext.Settings.AddAsync(new Setting { Name = "Setting1", Value = "value1" });
        }
    }
}
