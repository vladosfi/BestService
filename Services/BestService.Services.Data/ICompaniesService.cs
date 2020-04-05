namespace BestService.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompaniesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task<int> AddAsync(string name, string description, string image, string officialSite, string userId, string categoryId);

        T GetById<T>(int id);

        Task<int> EditAsync(string name, string description, string image, string officialSite, string categoryId);
    }
}
