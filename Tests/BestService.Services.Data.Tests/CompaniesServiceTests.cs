namespace BestService.Services.Data.Tests
{
    using System;

    using BestService.Data;
    using BestService.Data.Models;
    using BestService.Data.Repositories;
    using BestService.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
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
    }
}
