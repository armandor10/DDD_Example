using System;
using hey_url_challenge_code_dotnet.Application.DTOs;
using MediatR;

namespace hey_url_challenge_code_dotnet.Application.Commands.Visits
{
    public class CreateOrUpdateVisitCommand : IRequest<Guid>
    {
        public UrlDto UrlDto { get; set; }
        public VisitDto VisitDto { get; set; }
    }
}

