using System;
using hey_url_challenge_code_dotnet.Application.DTOs;
using hey_url_challenge_code_dotnet.Application.Queries.Url;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using hey_url_challenge_code_dotnet.Infra.DataContract;
using hey_url_challenge_code_dotnet.Infra.Data.Repositories;
using System.Linq;

namespace hey_url_challenge_code_dotnet.Application.Handlers.Url
{
    public class GetUrlByShortUrlQueryHandler : IRequestHandler<GetUrlByShortUrlQuery, UrlDto>
    {
        private readonly IUrlRepository _urlRepository;

        public GetUrlByShortUrlQueryHandler(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<UrlDto> Handle(GetUrlByShortUrlQuery request, CancellationToken cancellationToken)
        {
            return (await _urlRepository.GetAsync())
                .Where(x => x.ShortUrl == request.ShortUrl)
                .Select(x => new UrlDto
                {
                    Id = x.Id,
                    ShortUrl = x.ShortUrl,
                    OriginalUrl = x.OriginalUrl,
                    CreatedOn = x.CreatedOn
                })
                .FirstOrDefault();
        }
    }
}

