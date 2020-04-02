namespace BestService.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Models;

    public class CompaniesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Companies.Any())
            {
                return;
            }

            var companies = new List<(string Name, string Description, float Rating, string ImageUrl, string OfficialSite)>
            {
                 ("MentorMate Bulgaria Ltd.", "An industry veteran, MentorMate meets complex business challenges with native, hybrid, and custom software development. We think big, design smart, and develop fast for all screens, projects, and teams. We scale quickly and manage all stages of the software lifecycle, including ideation, architecture, design, development, and quality assurance.We have been developing software solutions since 2001 and have established skills across 30+ development languages and 10 technical practices. Our 500-people-strong software development team works closely with our clients to build effective and robust experiences, which include client-server solutions, mobile and desktop applications, modules and libraries, and cross-browser web-based solutions.With our headquarters in Minneapolis, Minnesota, we have an office in Gothenburg, Sweden, and five development offices in Bulgaria — in Sofia, Plovdiv, Varna, Veliko Tarnovo, and Ruse. In addition, we offer full-time remote opportunities to those who would like to work for us from any part of the world, as well as part - time options for contractors through the MentorMate Partner Network platform.", 3, "https://www.jobs.bg/company/22149", "https://mentormate.com/bg/"),
            };

            var categoryIds = dbContext.Categories.Select(x => x.Id).ToList();

            foreach (var categoryId in categoryIds)
            {
                foreach (var company in companies)
                {
                    await dbContext.Companies.AddAsync(new Company
                    {
                        Name = company.Name,
                        Description = company.Description,
                        Rating = company.Rating,
                        LogoImage = company.ImageUrl,
                        OfficialSite = company.OfficialSite,
                        CategoryId = categoryId,
                    });
                }
            }
        }
    }
}
