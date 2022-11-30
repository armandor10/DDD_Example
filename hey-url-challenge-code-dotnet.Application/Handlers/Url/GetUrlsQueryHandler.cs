using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.Application.DTOs;
using hey_url_challenge_code_dotnet.Application.Queries.Url;
using hey_url_challenge_code_dotnet.Infra.Data.Repositories;
using hey_url_challenge_code_dotnet.Infra.DataContract;
using MediatR;

namespace hey_url_challenge_code_dotnet.Application.Handlers.Url
{
    public class GetUrlsQueryHandler : IRequestHandler<GetUrlsQuery, List<UrlDto>>
    {
        private IUrlRepository _urlRepository;
        private IVisitsRepository _visitsRepository;

        public GetUrlsQueryHandler(IUrlRepository urlRepository, IVisitsRepository visitsRepository)
        {
            _urlRepository = urlRepository;
            _visitsRepository = visitsRepository;
        }

        public async Task<List<UrlDto>> Handle(GetUrlsQuery request, CancellationToken cancellationToken)
        {
            var urls = await _urlRepository.GetAsync();

            return urls.Select(u => new UrlDto {
                ShortUrl = u.ShortUrl,
                OriginalUrl = u.OriginalUrl,
                Id = u.Id,
                CreatedOn = u.CreatedOn,
                Count = getVisitsNumber(u.Id).Result
            }).ToList();
        }

        private async Task<int> getVisitsNumber(Guid urlId) => (await _visitsRepository.GetAsync())
                    .Where(v => v.UrlId == urlId)
                    .Select(v => v.Counter)
                    .Sum();
        
    }
}

