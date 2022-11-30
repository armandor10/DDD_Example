using System;
using hey_url_challenge_code_dotnet.Commons;
using hey_url_challenge_code_dotnet.Domain.Entities;
using NUnit.Framework;

namespace hey_url_challenge_code_dotnet.Domain.Tests.Entities
{
    public class UrlUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Create_Url_Instance()
        {
            // Arrange
            string originalUrl = "https://www.linkedin.com/in/diego-prens/";
            Url url;
            // Act
            url = new(originalUrl);
            // Asserts
            Assert.NotNull(url);
            Assert.AreNotEqual(Guid.Empty, url.Id);
            Assert.AreEqual(originalUrl, url.OriginalUrl);
            Assert.False(string.IsNullOrEmpty(url.ShortUrl));
        }

        [Test]
        public void Create_Url_Instance_ThrowsDomainExceptionValidation()
        {
            // Arrange
            string originalUrl = "";
            // Act and Asserts
            Assert.Throws<DomainExceptionValidation>(() => new Url(originalUrl));
        }
    }
}

