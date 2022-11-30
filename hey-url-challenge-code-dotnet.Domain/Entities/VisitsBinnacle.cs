using System;
using hey_url_challenge_code_dotnet.Commons;

namespace hey_url_challenge_code_dotnet.Domain.Entities
{
    public class VisitsBinnacle : Entity
    {
        public Guid VisiId { get; private set; }
        public string Browser { get; private set; }
        public string OS { get; private set; }

        private VisitsBinnacle ()
        {
        }

        public VisitsBinnacle (Guid visitId, string browser, string os)
        {
            VisiId = visitId;
            Browser = browser;
            OS = os;
        }
    }
}

