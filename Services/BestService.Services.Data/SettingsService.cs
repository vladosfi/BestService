namespace BestService.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using BestService.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class SettingsService : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public async Task<int> GetCountAsync() => await this.settingsRepository.AllAsNoTracking().CountAsync();

        public IEnumerable<T> GetAll<T>() => this.settingsRepository.All().To<T>().ToList();
    }
}
