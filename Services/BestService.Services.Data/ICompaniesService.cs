namespace BestService.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BestService.Data.Models;

    public interface ICompaniesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task<int> GetCountAsync();

        IEnumerable<T> GetByPages<T>(int? take = null, int skip = 0);

        Task<int> AddAsync(string name, string description, string image, string officialSite, string userId, int categoryId);

        T GetById<T>(int id);

        Task EditById(int id, string name, string description, string logoImage, string officialSite, int categoryId);

        Task<int> EditAsync(Company company);

        IEnumerable<T> GetRecentlyAdded<T>(int? count = null);

        IEnumerable<T> GetMostCommented<T>(int? count = null);

        IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0);

        int GetCountByCategoryId(int categoryId);
    }
}
