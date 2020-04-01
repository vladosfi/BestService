namespace BestService.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new Dictionary<string, string>
            {
                { "IT", "The information technology (IT) is comprised of companies that produce software, hardware or semiconductor equipment, or companies that provide internet or related services." },
                { "Healthcare", "Businesses that provide medical services, manufacture medical equipment or drugs, provide medical insurance, or otherwise facilitate the provision of healthcare to patients." },
                { "Transport", "Companies that provide services to move people or goods, as well as transportation infrastructure." },
                { "Other sectors", "Anyting else not included in other categories" },
                { "Entertainment", "Providing entertainment: radio and television and films and theater" },
                { "Agriculture", "Produces livestock, poultry, fish and crops." },
                { "Art", "Decorative applied arts objects used to enhance daily life and the interiors of homes. Such objects include items of clothing, fabrics for clothing and upholstery" },
                { "Education", "Establishments that provide instruction and training on a wide variety of subjects." },
                { "Sport", "Market in which people, activities, business, and organizations are involved in producing, facilitating, promoting, or organizing any activity, experience, or business enterprise focused on sports." },
                { "Construction", "The branch of manufacture and trade based on the building, maintaining, and repairing structures." },
                { "Tourism", "All about providing necessary means to assist tourists throughout their travelling." },
                { "Trade", "Involves the transfer of goods or services from one person or entity to another, often in exchange for money." },
                { "Services", "Part of the economy that creates services rather than tangible objects." },
                { "Properties", "Buying and selling of properties used as homes or for non-professional purposes." },
            };

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = category.Key,
                    Description = category.Value,
                });
            }
        }
    }
}
