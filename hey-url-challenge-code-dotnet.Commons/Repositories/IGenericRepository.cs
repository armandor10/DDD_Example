using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Commons.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> RemoveEntityAsync(TEntity entity);
        Task<TEntity> RemoveEntityAsync(Guid id);
    }
}

