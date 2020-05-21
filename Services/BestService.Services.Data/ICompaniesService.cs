namespace BestService.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BestService.Web.ViewModels.Tags;

    public interface ICompaniesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task<int> GetCountAsync();

        Task<IEnumerable<T>> SearchText<T>(string propertyReference, string freeText, int? take = null, int skip = 0, string sortOrder = null);

        Task<IEnumerable<T>> SearchByTag<T>(string text);

        Task<int> GetSearchCountAsync(string propertyReference, string serchedText);

        Task<IEnumerable<T>> GetByPages<T>(int? take = null, int skip = 0, string sortOrder = null);

        Task<int> AddAsync(string name, string description, string image, string officialSite, string userId, int categoryId);

        T GetById<T>(int id);

        Task<int> EditById(int id, string name, string description, string logoImage, string officialSite, int categoryId);

        IEnumerable<T> GetRecentlyAdded<T>(int? count = null);

        IEnumerable<T> GetMostCommented<T>(int? count = null);

        IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0);

        int GetCountByCategoryId(int categoryId);

        T GetByName<T>(string companyName);

        TagCloud GetTagCloud(int companyId);
    }
}
