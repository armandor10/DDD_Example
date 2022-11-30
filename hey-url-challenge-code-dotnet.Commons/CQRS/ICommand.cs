using System;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Commons.CQRS
{
    public interface ICommand
    {
        Guid Id { get; }
    }

    public interface ICommandHandler<T, R> where T : ICommand
    {
        Task<R> Handle(T command);
    }
}

