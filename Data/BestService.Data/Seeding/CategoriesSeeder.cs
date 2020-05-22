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
            var categories = new List<(string Name, string Description, string LogoImage)>
            {
                ("IT", "The information technology (IT) is comprised of companies that produce software, hardware or semiconductor equipment, or companies that provide internet or related services.", "v1587617166/Categories/IT_w2i8w4.jpg"),
                ("Healthcare", "Businesses that provide medical services, manufacture medical equipment or drugs, provide medical insurance, or otherwise facilitate the provision of healthcare to patients.", "v1587617166/Categories/HealthCare_mstcu0.jpg"),
                ("Transport", "Companies that provide services to move people or goods, as well as transportation infrastructure.", "v1587617166/Categories/Transport_mfpisb.jpg"),
                ("Other sectors", "Anyting else not included in other categories", "v1587617166/Categories/Other_cygccv.jpg"),
                ("Entertainment", "Providing entertainment: radio and television and films and theater", "v1587617166/Categories/Entertainment_qhqonl.png"),
                ("Agriculture", "Produces livestock, poultry, fish and crops.", "v1587617165/Categories/Agriculture_bisgvb.jpg"),
                ("Art", "Decorative applied arts objects used to enhance daily life and the interiors of homes. Such objects include items of clothing, fabrics for clothing and upholstery", "v1587617166/Categories/Art_rqtqqs.jpg"),
                ("Education", "Establishments that provide instruction and training on a wide variety of subjects.", "v1587617165/Categories/Education_ngssdh.jpg"),
                ("Sport", "Market in which people, activities, business, and organizations are involved in producing, facilitating, promoting, or organizing any activity, experience, or business enterprise focused on sports.", "v1587617165/Categories/Sport_icudbd.jpg"),
                ("Construction", "The branch of manufacture and trade based on the building, maintaining, and repairing structures.", "v1587617166/Categories/Construction_uyxbtj.jpg"),
                ("Tourism", "All about providing necessary means to assist tourists throughout their travelling.", "v1587617165/Categories/Tourism_tfdugc.jpg"),
                ("Trade", "Involves the transfer of goods or services from one person or entity to another, often in exchange for money.", "v1587617165/Categories/Trade_ueunf4.jpg"),
                ("Services", "Part of the economy that creates services rather than tangible objects.", "v1587617165/Categories/Services_km28di.png"),
                ("Properties", "Buying and selling of properties used as homes or for non-professional purposes.", "v1587617165/Categories/Properties_kytbmd.png"),
            };

            var ctgy = new List<Category>();

            foreach (var category in categories)
            {
                ctgy.Add(new Category
                {
                    Name = category.Name,
                    Description = category.Description,
                    LogoImage = category.LogoImage,
                });
            }

            await dbContext.Categories.AddRangeAsync(ctgy);
        }
    }
}
