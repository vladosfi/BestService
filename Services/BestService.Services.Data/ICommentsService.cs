namespace BestService.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task<int> CreateAsync(string content, string userId, int companyId, byte rating, int? parrentId = null);

        bool IsInCompanyId(int commentId, int companyId);

        Task<int> GetCountAsync();

        IEnumerable<T> Get<T>(int? count = null);
    }
}
