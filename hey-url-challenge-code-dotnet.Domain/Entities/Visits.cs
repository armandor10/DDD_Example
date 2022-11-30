using System;
using hey_url_challenge_code_dotnet.Commons;

namespace hey_url_challenge_code_dotnet.Domain.Entities
{
    public class Visits: Entity
    {
        public Guid UrlId { get; private set; }
        public DateTime VisitDay { get; private set; }
        public int Counter { get; private set; }

        private Visits()
        {
        }

        public Visits(Guid urlId)
        {
            this.UrlId = urlId;
            this.VisitDay = DateTime.Now;
            this.Counter = 1;
        }

        public void CountNewVisit() => Counter++;
    }
}

