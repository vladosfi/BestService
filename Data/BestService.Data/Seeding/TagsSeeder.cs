namespace BestService.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Models;

    public class TagsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var companies = dbContext.Companies.ToList();
            var tags = new List<Tag>();

            foreach (var company in companies)
            {
                tags.Add(new Tag
                {
                    Title = company.Category.Name,
                    CompanyId = company.Id,
                });
            }

            await dbContext.Tags.AddRangeAsync(tags);
        }
    }
}
