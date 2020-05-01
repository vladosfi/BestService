namespace BestService.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data;
    using BestService.Data.Models;
    using BestService.Data.Repositories;
    using BestService.Services.Mapping;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CategoriesServiceTest
    {
        [Fact]
        public async Task GetAllAsyncShouldReturnInCorrectCompanyObject()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));

            for (int i = 0; i < 1; i++)
            {
                await repository.AddAsync(new Category
                {
                    Name = $"CategoryName{i}",
                    Id = i + 1,
                    Description = $"CompanyDescription{i}",
                });
            }

            await repository.SaveChangesAsync();

            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestCategory).Assembly);

            var categories = categoryService.GetAll<MyTestCategory>(null).ToList();

            var category = new MyTestCategory
            {
                Name = $"CategoryName0",
                Id = 1,
                Description = $"CompanyDescription0",
            };

            category.Should().BeEquivalentTo(categories[0]);
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnInCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));

            for (int i = 0; i < 10; i++)
            {
                await repository.AddAsync(new Category { Name = $"CategoryName{i}" });
            }

            await repository.SaveChangesAsync();

            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestCategory).Assembly);

            var categories = categoryService.GetAll<MyTestCategory>(null);

            Assert.Equal(10, categories.Count());
        }

        [Fact]
        public async Task GetCountAsyncShouldReturnInCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));

            for (int i = 0; i < 1; i++)
            {
                await repository.AddAsync(new Category { Name = $"CategoryName{i}" });
            }

            await repository.SaveChangesAsync();

            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestCategory).Assembly);

            var categoriesCount = await categoryService.GetCountAsync();

            Assert.NotEqual(2, categoriesCount);
        }

        [Fact]
        public async Task GetCountAsyncShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));

            for (int i = 0; i < 3; i++)
            {
                await repository.AddAsync(new Category { Name = $"CategoryName{i}" });
            }

            await repository.SaveChangesAsync();

            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestCategory).Assembly);

            var categoriesCount = await categoryService.GetCountAsync();

            Assert.Equal(3, categoriesCount);
        }

        [Fact]
        public async Task GetByNameSouldReturnCorrectName()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Category { Name = $"CategoryName1" });
            await repository.SaveChangesAsync();

            var categoryService = new CategoriesService(repository);

            AutoMapperConfig.RegisterMappings(typeof(MyTestCategory).Assembly);

            var category = categoryService.GetByName<MyTestCategory>("CategoryName1");

            Assert.Equal("CategoryName1", category.Name);
        }

        [Fact]
        public async Task GetByNameSouldReturnInCorrectName()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Category { Name = "CategoryName1" });
            await repository.SaveChangesAsync();

            var categoryService = new CategoriesService(repository);

            AutoMapperConfig.RegisterMappings(typeof(MyTestCategory).Assembly);

            var category = categoryService.GetByName<MyTestCategory>("CategoryName1");

            Assert.NotEqual("Category", category.Name);
        }
    }

    public class MyTestCategory : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
