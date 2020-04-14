namespace BestService.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task<int> CreateAsync(string content, string userId, int companyId, byte rating, int? parrentId = null);
    }
}
