namespace BestService.Data.Common.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Common.Models;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        Task<TEntity> GetByIdWithDeletedAsync(params object[] id);

        IQueryable<TEntity> FullTextSearch(string propertyReference, string freeText);

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
