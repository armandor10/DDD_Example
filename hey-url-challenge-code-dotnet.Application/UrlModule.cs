using System;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace hey_url_challenge_code_dotnet.Application
{
    public static class UrlModule
    {
        public static IServiceCollection AddUrlModule(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(UrlModule).Assembly);

            return serviceCollection;
        }
    }
}

