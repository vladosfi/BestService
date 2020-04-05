namespace BestService.Services.Data
{
    using System.Threading.Tasks;

    public interface IVisitsService
    {
        long GetCompanyVisitCount(int companyId);

        Task<long> IncreaseVisit(int companyId, string userId);
    }
}
