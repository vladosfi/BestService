namespace BestService.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class RatingsService : IRatingsService
    {
        private readonly IRepository<Rate> ratingRepository;

        public RatingsService(
            IDeletableEntityRepository<Rate> ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        public async Task RateAsync(int companyId, string userId, int stars)
        {
            if (stars < 1 || stars > 5)
            {
                return;
            }

            var rating = this.ratingRepository.All().FirstOrDefault(x => x.CompanyId == companyId && x.UserId == userId);

            if (rating == null)
            {
                rating = new Rate
                {
                    CompanyId = companyId,
                    UserId = userId,
                    Stars = stars,
                };

                await this.ratingRepository.AddAsync(rating);
            }
            else
            {
                rating.Stars = stars;
                this.ratingRepository.Update(rating);
            }

            await this.ratingRepository.SaveChangesAsync();
        }

        public async Task<double> GetAvgCompanyRate(int companyId)
        {
            var averageStars = await this.ratingRepository.AllAsNoTracking().Where(x => x.CompanyId == companyId).AverageAsync(x => x.Stars);

            return averageStars;
        }

        public int GetCompanyRates(int companyId)
            => this.ratingRepository.AllAsNoTracking().Where(x => x.CompanyId == companyId).Count();

        public int GetAllCount() => this.ratingRepository.AllAsNoTracking().Count();

        public bool IsRateAllowed(int companyId, string userId)
            => !this.ratingRepository
            .AllAsNoTracking()
            .Any(x => x.CompanyId == companyId && x.UserId == userId);
    }
}
