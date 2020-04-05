namespace BestService.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class CompaniesService : ICompaniesService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;

        public CompaniesService(
            IDeletableEntityRepository<Company> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Company> query = this.companyRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<int> AddAsync(string name, string description, string logoImg, string officialSite, string userId, string categoryId)
        {
            var company = new Company
            {
                Name = name,
                Description = description,
                LogoImage = logoImg,
                OfficialSite = officialSite,
                UserId = userId,
                CategoryId = categoryId,
            };

            await this.companyRepository.AddAsync(company);
            await this.companyRepository.SaveChangesAsync();
            return company.Id;
        }

        public T GetById<T>(int id)
        {
            var company = this.companyRepository.All().Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return company;
        }
    }
}
