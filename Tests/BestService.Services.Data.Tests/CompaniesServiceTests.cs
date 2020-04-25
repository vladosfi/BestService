namespace BestService.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    using BestService.Data;
    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using BestService.Data.Repositories;
    using BestService.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class CompaniesServiceTests
    {
        [Fact]
        public void TestGetCompanyById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            AutoMapperConfig.RegisterMappings(typeof(MyTestCompany).Assembly);
            var repository = new EfDeletableEntityRepository<Company>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Company { Name = "test" }).GetAwaiter().GetResult();

            repository.SaveChangesAsync();
            var companyService = new CompaniesService(repository);
            var company = companyService.GetById<MyTestCompany>(1);
            Assert.Equal("test", company.Name);
        }

        public class MyTestCompany : IMapFrom<Company>
        {
            public string Name { get; set; }
        }


        [Theory]
        [InlineData("companyName", "Description", "logoImg", "officialSite", "userId", 1)]
        public async Task AddCompanyShouldReturnCorrectDataFromDbContext(
            string name,
            string description,
            string logoImg,
            string officialSite,
            string userId,
            int categoryId)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Company>(dbContext);
            var service = new CompaniesService(repository);
            int companyId = await service.AddAsync(name, description, logoImg, officialSite, userId, categoryId);
            var result = service.GetById(companyId);

            Assert.Equal(1, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(description, result.Description);
            Assert.Equal(logoImg, result.LogoImage);
            Assert.Equal(officialSite, result.OfficialSite);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(categoryId, result.CategoryId);
        }


        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Companies.Add(new Company());
            dbContext.Companies.Add(new Company());
            dbContext.Companies.Add(new Company());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Company>(dbContext);
            var service = new CompaniesService(repository);
            Assert.Equal(3, await service.GetCountAsync());
        }

        [Fact]
        public async Task GetAllShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Companies.Add(new Company());
            dbContext.Companies.Add(new Company());
            dbContext.Companies.Add(new Company());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Company>(dbContext);
            var service = new CompaniesService(repository);
            Assert.Equal(3, await service.GetCountAsync());
        }
    }
}
