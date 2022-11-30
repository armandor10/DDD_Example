using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace hey_url_challenge_code_dotnet.Commons.Repositories
{
    public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = await _dbSet.AddAsync(entity);
            return entityEntry.Entity;
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            IEnumerable<TEntity> entities = _dbSet;
            return entities;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            TEntity entity = await _dbSet.FindAsync(id);
            if (entity == null)
                entity = _dbSet.Local.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public Task<TEntity> RemoveEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> RemoveEntityAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = _dbSet.Update(entity);
            return entityEntry.Entity;
        }
    }
}

