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

        public T GetByIdTemplate<T>(int id)
        {
            var company = this.companyRepository
                .All()
                .Where(x => x.Id == id && x.IsDeleted == false)
                .To<T>()
                .FirstOrDefault();

            return company;
        }

        public Company GetById(int id)
        {
            return this.companyRepository
                .All()
                .Where(x => x.Id == id && x.IsDeleted == false)
                .FirstOrDefault();
        }

        public async Task EditById(int id, string name, string description, string logoImage, string officialSite, string categoryId)
        {
            var company = this.GetById(id);

            company.Name = name;
            company.Description = description;
            company.OfficialSite = officialSite;
            company.CategoryId = categoryId;
            if (logoImage != string.Empty)
            {
                company.LogoImage = logoImage;
            }

            this.companyRepository.Update(company);
            await this.companyRepository.SaveChangesAsync();
        }

        public async Task<int> EditAsync(Company company)
        {
            this.companyRepository.Update(company);
            await this.companyRepository.SaveChangesAsync();
            return company.Id;
        }

        public IEnumerable<T> GetRecentlyAdded<T>(int? count = null)
        {
            IQueryable<Company> companies = this.companyRepository
                .All()
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedOn);

            if (count.HasValue)
            {
                companies = companies.Take(count.Value);
            }

            return companies.To<T>().ToList();
        }

        public IEnumerable<T> GetMostCommented<T>(int? count = null)
        {
            IQueryable<Company> companies = this.companyRepository
                .All()
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Comments.Count);

            if (count.HasValue)
            {
                companies = companies.Take(count.Value);
            }

            return companies.To<T>().ToList();
        }
    }
}
