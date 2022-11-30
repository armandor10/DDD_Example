using System;
using hey_url_challenge_code_dotnet.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Application.Queries.Visits
{
    public class GetVisitMetricsQuery: IRequest<VisitMetricsDto>
    {
        public Guid UrlId { get; set; }
    }
}

