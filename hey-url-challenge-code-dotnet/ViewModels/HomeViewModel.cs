using System.Collections.Generic;
using hey_url_challenge_code_dotnet.Models;
using Microsoft.AspNetCore.Components;

namespace hey_url_challenge_code_dotnet.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Url> Urls { get; set; }
        public Url NewUrl { get; set; }
        public string OriginalUrl { get; set; }
        public string CurrentServerUrl { get; set; }
    }
}
