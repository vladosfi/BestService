namespace BestService.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using BestService.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CompaniesService : ICompaniesService
    {
        private readonly IDeletableEntityRepository<Company> companyRepository;
        private readonly IDeletableEntityRepository<Tag> tagRepository;

        public CompaniesService(
            IDeletableEntityRepository<Company> companyRepository,
            IDeletableEntityRepository<Tag> tagRepository)
        {
            this.companyRepository = companyRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IEnumerable<T>> SearchByTagAsync<T>(string text, int? take = null, int skip = 0, string sortOrder = null)
        {
            IQueryable<Company> query = this.companyRepository.All()
                .Where(c => c.CompanyTags.Any(x => x.Title == text))
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.To<T>().ToListAsync();
        }

        public async Task<int> SearchByTagCountAsync(string text)
            => await this.companyRepository.All()
                        .Where(c => c.CompanyTags.Any(x => x.Title == text))
                        .CountAsync();

        public async Task<IEnumerable<T>> SearchTextAsync<T>(string propertyReference, string serchedText, int? take = null, int skip = 0, string sortOrder = null)
        {
            IQueryable<Company> query = null;

            query = sortOrder switch
            {
                "Name_asc" => this.companyRepository
                                       .FullTextSearch(propertyReference, serchedText)
                                       .OrderBy(c => c.Name)
                                       .Skip(skip),
                "Name_desc" => this.companyRepository
                                        .FullTextSearch(propertyReference, serchedText)
                                        .OrderByDescending(c => c.Name)
                                        .Skip(skip),
                "Oldest" => this.companyRepository
                                        .FullTextSearch(propertyReference, serchedText)
                                        .OrderBy(c => c.Name)
                                        .Skip(skip),
                _ => this.companyRepository
                                        .FullTextSearch(propertyReference, serchedText)
                                        .OrderByDescending(c => c.CreatedOn)
                                        .Skip(skip),
            };

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.To<T>().ToListAsync();
        }

        public async Task<int> GetSearchCountAsync(string propertyReference, string serchedText)
            => await this.companyRepository
                        .FullTextSearch(propertyReference, serchedText)
                        .CountAsync();

        public async Task<IEnumerable<T>> GetByPages<T>(int? take = null, int skip = 0, string sortOrder = null)
        {
            IQueryable<Company> query = null;

            query = sortOrder switch
            {
                "Name_asc" => this.companyRepository
                                       .AllAsNoTracking()
                                       .OrderBy(c => c.Name)
                                       .Skip(skip),
                "Name_desc" => this.companyRepository
                                        .AllAsNoTracking()
                                        .OrderByDescending(c => c.Name)
                                        .Skip(skip),
                "Oldest" => this.companyRepository
                                        .AllAsNoTracking()
                                        .OrderBy(c => c.Name)
                                        .Skip(skip),
                _ => this.companyRepository
                                        .AllAsNoTracking()
                                        .OrderByDescending(c => c.CreatedOn)
                                        .Skip(skip),
            };

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.To<T>().ToListAsync();
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

        public async Task<int> AddAsync(string name, string description, string logoImg, string officialSite, string userId, int categoryId)
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

        public async Task<int> GetCountAsync() => await this.companyRepository.AllAsNoTracking().CountAsync();

        public T GetById<T>(int id)
            => this.companyRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

        public Company GetById(int id)
            => this.companyRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

        public async Task<int> EditById(int id, string name, string description, string logoImage, string officialSite, int categoryId)
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
            return await this.companyRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetRecentlyAdded<T>(int? count = null)
        {
            IQueryable<Company> companies = this.companyRepository
                .All()
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
                .OrderByDescending(x => x.Comments.Count);

            if (count.HasValue)
            {
                companies = companies.Take(count.Value);
            }

            return companies.To<T>().ToList();
        }

        public IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0)
        {
            var query = this.companyRepository
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .Where(c => c.CategoryId == categoryId)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public int GetCountByCategoryId(int categoryId) => this.companyRepository.All().Count(c => c.CategoryId == categoryId);

        public Task<bool> Exist(int companyId)
        {
            return this.companyRepository.AllAsNoTracking().AnyAsync(x => x.Id == companyId);
        }

        public T GetByName<T>(string companyName)
        {
            return this.companyRepository.AllAsNoTracking().Where(c => c.Name == companyName).To<T>().FirstOrDefault();
        }

        /// <summary>
        /// TagClud.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetTags(int companyId)
        {
            var tags = await this.tagRepository.AllAsNoTracking()
                .Where(t => t.CompanyId == companyId)
                .Select(t => t.Title)
                .ToListAsync();

            return tags;
        }
    }
}
