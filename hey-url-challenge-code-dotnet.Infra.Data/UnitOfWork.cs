using System;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.Commons.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace hey_url_challenge_code_dotnet.Infra.Data
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext: DbContext, IDisposable
    {
        public UnitOfWork(TContext context, ILogger<UnitOfWork<TContext>> logger)
        {
            Context = context ?? throw new ArgumentException(nameof(context));
            _logger = logger;
        }

        public TContext Context { get; }

        private ILogger<UnitOfWork<TContext>> _logger;

        public async Task CommitAsync()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to update database");
            }
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}

