using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.Application.DTOs;
using hey_url_challenge_code_dotnet.Application.Queries.Visits;
using hey_url_challenge_code_dotnet.Commons.Repositories;
using hey_url_challenge_code_dotnet.Infra.Data;
using hey_url_challenge_code_dotnet.Infra.Data.Repositories;
using hey_url_challenge_code_dotnet.Infra.DataContract;
using MediatR;

namespace hey_url_challenge_code_dotnet.Application.Handlers.Visit
{
    public class GetVisitMetricsQueryHandler: IRequestHandler<GetVisitMetricsQuery, VisitMetricsDto>
    {
        private readonly IVisitsRepository _visitsRepository;
        private readonly IVisitsBinnacleRepository _visitsBinnacleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetVisitMetricsQueryHandler(IVisitsRepository visitsRepository, IVisitsBinnacleRepository visitsBinnacleRepository)
        {
            _visitsRepository = visitsRepository;
            _visitsBinnacleRepository = visitsBinnacleRepository;
        }

        public async Task<VisitMetricsDto> Handle(GetVisitMetricsQuery request, CancellationToken cancellationToken)
        {
            VisitMetricsDto dto = new();
            var visits = (await _visitsRepository.GetAsync())
                                .Where(x => x.UrlId == request.UrlId &&
                                            x.VisitDay.Month == DateTime.Now.Month);

            dto.DailyClicks = visits.OrderBy(x => x.VisitDay)
                                .Select((visit, index) => new { index = (++index).ToString(), visit.Counter})
                                .ToDictionary(x => x.index, x => x.Counter);

            var visitsIds = visits.Select(v => v.Id).ToList();
            var visitBinacle = (await _visitsBinnacleRepository.GetAsync())
                                    .Where(x => visitsIds.Contains(x.VisiId));

            dto.BrowseClicks = visitBinacle
                                    .GroupBy(g => g.Browser)
                                    .Select(b => new { b.First().Browser, Counter = b.Count() })
                                    .ToDictionary(x => x.Browser, x => x.Counter);
            dto.PlatformClicks = visitBinacle
                                    .GroupBy(g => g.OS)
                                    .Select(b => new { b.First().OS, Counter = b.Count() })
                                    .ToDictionary(x => x.OS, x => x.Counter);
            return dto;
        }
    }
}

