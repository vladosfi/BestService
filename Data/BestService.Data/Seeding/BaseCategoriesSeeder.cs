namespace BestService.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Models;

    public class BaseCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.BaseCategories.Any())
            {
                return;
            }

            var baseCategories = new Dictionary<string, string>
            {
                { "Advertising", "Advertising is a marketing communication that employs an openly sponsored, non-personal message to promote or sell a product, service or idea." },
                { "IT", "The information technology (IT) is comprised of companies that produce software, hardware or semiconductor equipment, or companies that provide internet or related services." },
                { "Entertainment", "Entertainment is involved in providing entertainment: radio and television and films and theater" },
                { "Agriculture", "Agriculture is the sector of the economy that produces livestock, poultry, fish and crops." },
                { "Infrastructure", "Infrastructure are basic facilities, structures, equipment, technologies and services that act as the foundation for economic activity and quality of life" },
                { "Construction", "Construction is the sector of the economy that builds, improves and repairs buildings, structures, infrastructure and land features. The following are common components of the construction industry." },
                { "Healthcare",  "Healthcare sector consists of businesses that provide medical services, manufacture medical equipment or drugs, provide medical insurance, or otherwise facilitate the provision of healthcare to patients." },
                { "Education", "Educational Services is composed of establishments that provide instruction and training on a wide variety of subjects." },
                { "Transport", "The transportation sector is a category of companies that provide services to move people or goods, as well as transportation infrastructure. " },
                { "Fashion", "Fashion is a large industry that includes elements of design, manufacturing, marketing, supply chain, retailing, ecommerce and media. Products include apparel, footwear, accessories, cosmetics and materials such as textiles." },
            };

            foreach (var category in baseCategories)
            {
                await dbContext.BaseCategories.AddAsync(new BaseCategory
                {
                    Name = category.Key,
                    Description = category.Value,
                });
            }
        }
    }
}
