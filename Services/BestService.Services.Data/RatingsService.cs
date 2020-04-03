namespace BestService.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;

    public class RatingsService : IRatingsService
    {
        private readonly IRepository<Rate> ratingRepository;

        public RatingsService(IRepository<Rate> ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        public async Task RatingAsync(int companyId, string userId, int stars)
        {
            var rating = this.ratingRepository.All().FirstOrDefault(x => x.CompanyId == companyId && x.UserId == userId);

            if (rating != null)
            {
                rating.Stars = stars;
            }
            else
            {
                rating = new Rate
                {
                    CompanyId = companyId,
                    UserId = userId,
                    Stars = stars,
                };

                await this.ratingRepository.AddAsync(rating);
            }

            await this.ratingRepository.SaveChangesAsync();
        }
    }
}
