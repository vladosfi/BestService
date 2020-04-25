namespace BestService.Services.Data
{
    using System.Threading.Tasks;

    public interface IRatingsService
    {
        Task RateAsync(int companyId, string userId, int stars);

        Task<double> GetAvgCompanyRate(int companyId);

        int GetCompanyRates(int companyId);

        int GetCount();

        bool IsRateAllowed(int companyId, string userId);
    }
}
