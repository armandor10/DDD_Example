using System;
using System.Collections.Generic;
using hey_url_challenge_code_dotnet.Application.DTOs;
using MediatR;

namespace hey_url_challenge_code_dotnet.Application.Queries.Url
{
    public class GetUrlsQuery: IRequest<List<UrlDto>>
    {

    }
}

