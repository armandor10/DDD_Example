using System;
using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Application.DTOs
{
    public class VisitMetricsDto
    {
        public Dictionary<string, int> DailyClicks { get; set; }
        public Dictionary<string, int> BrowseClicks { get; set; }
        public Dictionary<string, int> PlatformClicks { get; set; }
    }
}

