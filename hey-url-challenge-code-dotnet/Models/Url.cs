using System;
using System.ComponentModel.DataAnnotations;

namespace hey_url_challenge_code_dotnet.Models
{
    public class Url
    {
        public Guid Id { get; set; }
        public string ShortUrl { get; set; }
        public int Count { get; set; }
        public string OriginalUrl { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime CreatedOn { get; set; }
    }
}
