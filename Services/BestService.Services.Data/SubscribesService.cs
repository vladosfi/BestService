namespace BestService.Services.Data
{
    using System.Threading.Tasks;

    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class SubscribesService : ISubscribesService
    {
        private readonly IDeletableEntityRepository<Subscribe> subscribeRepository;

        public SubscribesService(IDeletableEntityRepository<Subscribe> subscribeRepository)
        {
            this.subscribeRepository = subscribeRepository;
        }

        public async Task<int> GetCountAsync() => await this.subscribeRepository.AllAsNoTracking().CountAsync();

        public async Task Subscribe(string email, string ip)
        {
            var subscrib = new Subscribe
            {
                Email = email,
                Ip = ip,
            };

            await this.subscribeRepository.AddAsync(subscrib);
            await this.subscribeRepository.SaveChangesAsync();
        }
    }
}
