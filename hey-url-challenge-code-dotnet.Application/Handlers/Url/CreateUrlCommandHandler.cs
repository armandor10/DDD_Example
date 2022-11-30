using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.Application.Commands.Url;
using hey_url_challenge_code_dotnet.Commons.CQRS;
using hey_url_challenge_code_dotnet.Domain.Entities;
using hey_url_challenge_code_dotnet.Infra.DataContract;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using hey_url_challenge_code_dotnet.Commons.Repositories;

namespace hey_url_challenge_code_dotnet.Application.Handlers.Url
{
    public class CreateUrlCommandHandler: IRequestHandler<CreateUrlCommand, Guid>
    {
        private readonly IUrlRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUrlCommandHandler(IUrlRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateUrlCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Url url = new Domain.Entities.Url(request.UrlDto.OriginalUrl);
            while (true)
            {
                if (!(await _repository.GetAsync()).Select(x => x.ShortUrl).Any(x => x == url.ShortUrl))
                    break;
                else
                    url.GenerateNewShortUrl();
            }
            await _repository.CreateAsync(url);
            await _unitOfWork.CommitAsync();
            return url.Id;
        }
    } 
}

