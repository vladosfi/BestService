namespace BestService.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using BestService.Data;
    using BestService.Data.Models;
    using BestService.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    
    public class RatingServiceTests
    {
        [Fact]
        public async Task ManyRateShoiuldCountOnce()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockRepository = new EfDeletableEntityRepository<Rate>(new ApplicationDbContext(options.Options));
            var service = new RatingsService(mockRepository);

            await service.RateAsync(1, "UserId", 3);
            await service.RateAsync(1, "UserId", 3);

            var rates = service.GetAllCount();
            Assert.Equal(1, rates);
        }

        [Fact]
        public async Task TwoRatesFromDifferentUsersShoiuldCountTwoTimes()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockRepository = new EfDeletableEntityRepository<Rate>(new ApplicationDbContext(options.Options));
            var service = new RatingsService(mockRepository);

            await service.RateAsync(1, "UserId1", 3);
            await service.RateAsync(1, "UserId2", 3);

            var rates = service.GetAllCount();
            Assert.Equal(2, rates);
        }

        [Fact]
        public async Task TwoRateFromSameUserShoiuldUpdateLastRate()
        {
            var companyId = 1;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockRepository = new EfDeletableEntityRepository<Rate>(new ApplicationDbContext(options.Options));
            var service = new RatingsService(mockRepository);

            await service.RateAsync(companyId, "UserId1", 3);
            await service.RateAsync(companyId, "UserId1", 5);

            var avgRates = await service.GetAvgCompanyRate(companyId);
            Assert.Equal(5.0, avgRates);
        }

        [Fact]
        public async Task ManyRatesFromDifferentUsersShoiuldReturnCorrectAverageRate()
        {
            var companyId = 1;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockRepository = new EfDeletableEntityRepository<Rate>(new ApplicationDbContext(options.Options));
            var service = new RatingsService(mockRepository);

            await service.RateAsync(companyId, "UserId1", 3);
            await service.RateAsync(companyId, "UserId2", 5);
            await service.RateAsync(companyId, "UserId3", 4);

            var avgRates = await service.GetAvgCompanyRate(companyId);
            Assert.Equal(4.0, avgRates);
        }

        [Fact]
        public async Task IncorrectRateShoiuldReturnZeroCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockRepository = new EfDeletableEntityRepository<Rate>(new ApplicationDbContext(options.Options));
            var service = new RatingsService(mockRepository);

            await service.RateAsync(1, "UserId", 6);
            await service.RateAsync(1, "UserId", 0);

            var rates = service.GetAllCount();
            Assert.Equal(0, rates);
        }

        [Fact]
        public async Task GetCompanyRatesShoiuldReturnCorrectTotalRatesForCurrentCompany()
        {
            var companyId = 1;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockRepository = new EfDeletableEntityRepository<Rate>(new ApplicationDbContext(options.Options));
            var service = new RatingsService(mockRepository);

            await service.RateAsync(companyId, "UserId1", 3);
            await service.RateAsync(companyId, "UserId2", 5);
            await service.RateAsync(companyId, "UserId3", 4);

            var companyRatesCount = service.GetCompanyRates(companyId);
            Assert.Equal(3, companyRatesCount);
        }

        [Fact]
        public async Task UserCannotRateIfUserAlreayRate()
        {
            var companyId = 1;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockRepository = new EfDeletableEntityRepository<Rate>(new ApplicationDbContext(options.Options));
            var service = new RatingsService(mockRepository);

            var rateIsAllowed = service.IsRateAllowed(companyId, "UserId1");
            Assert.True(rateIsAllowed);

            await service.RateAsync(companyId, "UserId1", 3);

            var rateAllowed = service.IsRateAllowed(companyId, "UserId1");
            Assert.False(rateAllowed);
        }
    }
}
