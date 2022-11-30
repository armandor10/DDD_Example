using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.Application.Commands.Visits;
using hey_url_challenge_code_dotnet.Application.DTOs;
using hey_url_challenge_code_dotnet.Commons.Repositories;
using hey_url_challenge_code_dotnet.Domain.Entities;
using hey_url_challenge_code_dotnet.Infra.Data;
using hey_url_challenge_code_dotnet.Infra.Data.Repositories;
using hey_url_challenge_code_dotnet.Infra.DataContract;
using MediatR;

namespace hey_url_challenge_code_dotnet.Application.Handlers.Visits
{
    public class CreateOrUpdateVisitCommandHandler: IRequestHandler<CreateOrUpdateVisitCommand, Guid>
    {
        private IUrlRepository _urlRepository;
        private IVisitsRepository _visitsRepository;
        private IVisitsBinnacleRepository _visitsBinnacleRepository;
        private IUnitOfWork _unitOfWork;

        public CreateOrUpdateVisitCommandHandler(IUrlRepository urlRepository, IVisitsRepository visitsRepository, IVisitsBinnacleRepository visitsBinnacleRepository, IUnitOfWork unitOfWork)
        {
            _urlRepository = urlRepository;
            _visitsRepository = visitsRepository;
            _visitsBinnacleRepository = visitsBinnacleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateOrUpdateVisitCommand request, CancellationToken cancellationToken)
        {
            Guid? urlId = (await _urlRepository.GetAsync())
                            .FirstOrDefault(x => x.ShortUrl == request.UrlDto.ShortUrl)
                            ?.Id;
            if (urlId.HasValue)
            {
                await CreateOrUpdateVisit(urlId, request.VisitDto);
                return urlId.Value;
            }
            else
                return Guid.Empty;
        }

        private async Task CreateOrUpdateVisit(Guid? urlId, VisitDto visitDto)
        {
            var visit = (await _visitsRepository.GetAsync())
                .FirstOrDefault(x => x.UrlId == urlId.Value && x.VisitDay.Date == DateTime.Now.Date);
            if (visit != null)
                visit.CountNewVisit();
            else
            {
                visit = new Domain.Entities.Visits(urlId.Value);
                await _visitsRepository.CreateAsync(visit);
            }
            await _unitOfWork.CommitAsync();
            await CreateVisitBinnacle(visit.Id, visitDto);
        }

        private async Task CreateVisitBinnacle(Guid visitId, VisitDto visitDto) {
            VisitsBinnacle visitsBinnacle = new VisitsBinnacle(visitId, visitDto.BrowserName, visitDto.BrowserOS);
            await _visitsBinnacleRepository.CreateAsync(visitsBinnacle);
            await _unitOfWork.CommitAsync();
        }
    }
}

