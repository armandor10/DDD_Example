using System;
using hey_url_challenge_code_dotnet.Commons.Repositories;
using hey_url_challenge_code_dotnet.Domain.Entities;

namespace hey_url_challenge_code_dotnet.Infra.DataContract
{
    public interface IUrlRepository: IGenericRepository<Url>
    {
    }
}

