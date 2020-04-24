namespace BestService.Services.Data
{
    using System.Threading.Tasks;

    public interface IRatingsService
    {
        Task RateAsync(int companyId, string userId, int stars);

        Task<double> GetAvgCompanyRate(int companyId);

        Task<int> GetCompanyRates(int companyId);

        Task<int> GetCountAsync();

        bool IsRateAllowed(int companyId, string userId);
    }
}
