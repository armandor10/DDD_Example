using System;
using System.Collections.Generic;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;
using MediatR;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.Application.Commands.Url;
using hey_url_challenge_code_dotnet.Application.DTOs;
using hey_url_challenge_code_dotnet.Application.Queries.Url;
using System.Linq;
using static System.Net.WebRequestMethods;
using hey_url_challenge_code_dotnet.Application.Commands.Visits;
using hey_url_challenge_code_dotnet.Application.Queries.Visits;
using HeyUrlChallengeCodeDotnet.Models;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private static readonly Random getrandom = new Random();
        private readonly IBrowserDetector browserDetector;
        private readonly IMediator _mediator;

        public UrlsController(ILogger<UrlsController> logger, IBrowserDetector browserDetector, IMediator mediator)
        {
            this.browserDetector = browserDetector;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();
            model.CurrentServerUrl = $"https://{HttpContext.Request.Host.Value}/";
            model.Urls = (await _mediator.Send(new GetUrlsQuery()))
                .Select(x => new Url
                {
                    ShortUrl = x.ShortUrl,
                    OriginalUrl = x.OriginalUrl,
                    Count = x.Count,
                    CreatedOn = x.CreatedOn
                }).ToList();
            model.NewUrl = new();
            return View(model);
        }

        [Route("Home/Error/{message}")]
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel
            {
                RequestId = "url1",
                ErrorMessage = $"Error Ocurred. {message}"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("OriginalUrl")] Url url)
        {
            // Create product
            var result = await _mediator.Send(new CreateUrlCommand() { UrlDto = new UrlDto { OriginalUrl = url.OriginalUrl } });
            return RedirectToAction(nameof(Index));
        }

        [Route("/{url}")]
        public async Task<IActionResult> Visit(string url)
        {
            var urlId = await _mediator.Send(new CreateOrUpdateVisitCommand() {
                UrlDto = new UrlDto {
                    ShortUrl = url
                },
                VisitDto = new VisitDto
                {
                    BrowserOS = this.browserDetector.Browser.OS,
                    BrowserName = this.browserDetector.Browser.Name
                }
            });
            if (urlId == Guid.Empty)
                return RedirectToAction(nameof(Error), new { message = $@"Invalid short url (https://{HttpContext.Request.Host.Value}/{url})" });
            else
                return new OkObjectResult($"{url}, {this.browserDetector.Browser.OS}, {this.browserDetector.Browser.Name}");
        }

        [Route("urls/{url}")]
        public async Task<IActionResult> Show(string url)
        {
            var urlDto = await _mediator.Send(new GetUrlByShortUrlQuery { ShortUrl = url });
            var visitMetrics = await _mediator.Send(new GetVisitMetricsQuery { UrlId = urlDto.Id });
            return View(new ShowViewModel
            {
                Url = new Url { ShortUrl = url, CreatedOn = urlDto.CreatedOn, OriginalUrl = urlDto.OriginalUrl },
                DailyClicks = visitMetrics.DailyClicks,
                BrowseClicks = visitMetrics.BrowseClicks,
                PlatformClicks = visitMetrics.PlatformClicks
            });
        }
    }
}