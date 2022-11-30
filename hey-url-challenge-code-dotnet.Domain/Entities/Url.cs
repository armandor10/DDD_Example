using System;
using System.Text;
using hey_url_challenge_code_dotnet.Commons;

namespace hey_url_challenge_code_dotnet.Domain.Entities
{
    public class Url: Entity
    {
        private const string LETTERS = "ABCDEFGHIJKMNOPQRSTUVWXYZ";
        private const int LENGHT_SHORT_URL = 5;
        //private const string BASE_URL = "https://localhost:5001/";

        public string ShortUrl { get; private set; }
        public string OriginalUrl { get; private set; }

        private Url()
        {
        }

        public Url(string originalUrl)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(originalUrl),
                                           DomainExceptionValidation.GetFieldRequiredMessage(nameof(originalUrl)));
            this.OriginalUrl = originalUrl;
            this.ShortUrl = GenerateShortUrl();
        }

        public void GenerateNewShortUrl() => this.ShortUrl = GenerateShortUrl();

        private string GenerateShortUrl()
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < LENGHT_SHORT_URL; i++)
                builder.Append(LETTERS[random.Next(0, LETTERS.Length - 1)]);
            return builder.ToString();
        }

    }
}

