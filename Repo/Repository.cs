using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Repo
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private bool _disposed = false;
        private bool disposedValue;
        private readonly StudentManagementDbContext _dbContext;
        private readonly DbSet<TEntity> _set;
        private IDbContextTransaction? _transaction { get; set; }

        public Repository(StudentManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            _set = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false)
        {
            IQueryable<TEntity> retVal = _set;

            if (filter != null)
                retVal = retVal.Where(filter);

            if (asNoTracking)
                retVal = retVal.AsNoTracking();

            return retVal;
        }

        public async Task<TEntity?> GetFirst(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false)
        {
            var query = GetAll(filter, asNoTracking);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity?> GetSingle(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false)
        {
            var query = GetAll(filter, asNoTracking);
            return await query.SingleOrDefaultAsync(); // throws Exception if multiple are there
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void EndTransaction(IDbContextTransaction transaction)
        {
            try
            {
                _transaction?.Commit();
            }
            catch (Exception ex)
            {
                _transaction?.Rollback();
                throw;
            }
        }
        
        public async Task<TEntity?> Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = _set.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<TEntity?> Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = _set.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }
        
        public async Task<TEntity?> Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = _set.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task AddCollection(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _set.AddRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCollection(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _set.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task DeleteCollection(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _set.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext?.Dispose();
                    _transaction?.Dispose();
                }

                GC.SuppressFinalize(this);
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }
    }
}
