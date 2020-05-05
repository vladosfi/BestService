namespace BestService.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISearchService
    {
        Task<List<string>> ByStringInNameAsync(string companyNameString);
    }
}
