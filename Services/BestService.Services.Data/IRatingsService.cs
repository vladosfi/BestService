namespace BestService.Services.Data
{
    using System.Threading.Tasks;

    public interface IRatingsService
    {
        Task RatingAsync(int companyId, string userId, int rateValue);
    }
}
