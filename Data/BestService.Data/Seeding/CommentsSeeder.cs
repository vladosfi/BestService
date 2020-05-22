namespace BestService.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Models;

    public class CommentsSeeder : ISeeder
    {
        private readonly Random random = new Random();

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var companies = dbContext.Companies.ToList();
            var users = dbContext.Users.ToList();
            var comments = new List<Comment>();
            var visits = new List<Visit>();

            foreach (var company in companies)
            {
                foreach (var user in users)
                {
                    var comment = new Comment
                    {
                        CompanyId = company.Id,
                        UserId = user.Id,
                        Content = this.GetRndCommentContent(),
                    };

                    comments.Add(comment);

                    var visit = new Visit()
                    {
                        CompanyId = company.Id,
                        UserId = user.Id,
                        Count = 1,
                    };

                    visits.Add(visit);
                }
            }

            await dbContext.Comments.AddRangeAsync(comments);
            await dbContext.Visits.AddRangeAsync(visits);
        }

        private string GetRndCommentContent()
        {
            List<string> commentsContent = new List<string>();

            commentsContent.Add("A superb keynote presentation and moderation that kept us through our pace during whole the day.");
            commentsContent.Add("The case study was particularly good because it was a real life example of a company entering a new market to think outside the box.");
            commentsContent.Add("Just wanted to let you guys know at a recent company manager meeting my email newsletter was used as a example showing how a great way a contractor keeps in touch with customers and other contacts. One of the regional managers for our area receives my email newsletter and is always impressed with the content and easy read. Thanks for the great work!");
            commentsContent.Add("Should have I hired these guys sooner. Everything They do is top notch. Customer service is great they respond right away to your emails and make any changes asap. Just had some marketing items printed and was very happy when they arrived early, and look excellent.");
            commentsContent.Add("Good company that has the most incredible response time. They are professional, proficient, knowledgable and on top of their game.");

            int index = this.random.Next(commentsContent.Count);

            return commentsContent[index];
        }
    }
}
