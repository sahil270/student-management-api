using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Repo
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        Task<TEntity?> Add(TEntity entity);
        Task AddCollection(IEnumerable<TEntity> entities);
        void BeginTransaction();
        Task<TEntity?> Delete(TEntity entity);
        Task DeleteCollection(IEnumerable<TEntity> entities);
        void EndTransaction(IDbContextTransaction transaction);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null, bool asNoTracking = false);
        Task<TEntity?> GetFirst(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false);
        Task<TEntity?> GetSingle(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false);
        Task<TEntity?> Update(TEntity entity);
        Task UpdateCollection(IEnumerable<TEntity> entities);
    }
}