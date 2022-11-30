using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace hey_url_challenge_code_dotnet.Commons.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    } 
}

