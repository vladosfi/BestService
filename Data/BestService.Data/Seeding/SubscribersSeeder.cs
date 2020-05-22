namespace BestService.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Common;
    using BestService.Data.Models;

    internal class SubscribersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var subscribers = new List<Subscribe>();

            for (int i = 0; i < 123; i++)
            {
                var subscriber = new Subscribe
                {
                    Email = i + GlobalConstants.AdminEmail,
                    Ip = GlobalConstants.LocalIp,
                };

                subscribers.Add(subscriber);
            }

            await dbContext.Subscribes.AddRangeAsync(subscribers);
        }
    }
}
