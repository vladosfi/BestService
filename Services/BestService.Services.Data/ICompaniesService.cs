namespace BestService.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BestService.Data.Models;

    public interface ICompaniesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task<int> AddAsync(string name, string description, string image, string officialSite, string userId, string categoryId);

        T GetByIdTemplate<T>(int id);

        Company GetById(int id);

        Task EditById(int id, string name, string description, string logoImage, string officialSite, string categoryId);

        Task<int> EditAsync(Company company);

        IEnumerable<T> GetRecentlyAdded<T>(int? count = null);

        IEnumerable<T> GetMostCommented<T>(int? count = null);
    }
}
