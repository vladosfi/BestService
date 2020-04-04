namespace BestService.Services.Data
{
    using System.Threading.Tasks;

    public interface IRatingsService
    {
        Task RateAsync(int companyId, string userId, int stars);

        int GetRating(int companyId);

        int GetAvgCompanyRate(int companyId);

        int GetCompanyReview(int companyId);
    }
}
