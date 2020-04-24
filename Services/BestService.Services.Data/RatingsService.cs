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

        public RatingsService(IRepository<Rate> ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        public async Task RateAsync(int companyId, string userId, int stars)
        {
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

        public async Task<int> GetCompanyRates(int companyId)
            => await this.ratingRepository.AllAsNoTracking().Where(x => x.CompanyId == companyId).CountAsync();

        public async Task<int> GetCountAsync() => await this.ratingRepository.AllAsNoTracking().CountAsync();

        public bool IsRateAllowed(int companyId, string userId)
            => !this.ratingRepository
            .AllAsNoTracking()
            .Any(x => x.CompanyId == companyId && x.UserId == userId);
    }
}
