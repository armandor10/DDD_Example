using System;
using hey_url_challenge_code_dotnet.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Application.Queries.Url
{
    public class GetUrlByShortUrlQuery: IRequest<UrlDto>
    {

        public string ShortUrl { get; set; }
    }
}

