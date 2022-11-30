using System;
using hey_url_challenge_code_dotnet.Commons.Repositories;
using hey_url_challenge_code_dotnet.Domain.Entities;
using hey_url_challenge_code_dotnet.Infra.DataContract;

namespace hey_url_challenge_code_dotnet.Infra.Data.Repositories
{
    public class VisitsRepository: GenericRepository<Visits>, IVisitsRepository
    {
        public VisitsRepository(ApplicationContext context) : base(context)
        {
        }
    }
}

