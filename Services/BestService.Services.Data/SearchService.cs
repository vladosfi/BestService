namespace BestService.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;

        public SearchService(IDeletableEntityRepository<Company> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task<List<string>> ByStringInNameAsync(string companyNameString)
        {
            var companyNameList = await this.companyRepository.AllAsNoTracking().Where(x => x.Name.Contains(companyNameString))
                    .OrderBy(x => x.Name)
                    .Select(x => x.Name).ToListAsync();

            return companyNameList;
        }
    }
}
