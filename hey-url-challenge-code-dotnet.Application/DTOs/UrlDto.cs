using System;
namespace hey_url_challenge_code_dotnet.Application.DTOs
{
    public class UrlDto
    {
        public Guid Id { get; set; }
        public string ShortUrl { get; set; }
        public string OriginalUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Count { get; set; }
    }
}

