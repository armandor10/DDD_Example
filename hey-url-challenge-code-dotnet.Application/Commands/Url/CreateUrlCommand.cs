using System;
using hey_url_challenge_code_dotnet.Application.DTOs;
using hey_url_challenge_code_dotnet.Commons.CQRS;
using MediatR;

namespace hey_url_challenge_code_dotnet.Application.Commands.Url
{
    public class CreateUrlCommand: IRequest<Guid>
    {
        public UrlDto UrlDto { get; set; }
    }
}

