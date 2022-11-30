using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.Application.Commands.Url;
using hey_url_challenge_code_dotnet.Application.Handlers.Url;
using hey_url_challenge_code_dotnet.Commons.Repositories;
using hey_url_challenge_code_dotnet.Infra.DataContract;
using Moq;
using NUnit.Framework;

namespace hey_url_challenge_code_dotnet.Application.Tests
{


    public class CreateUrlCommandHandlerTests
    {
        private Mock<IUrlRepository> _urlRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private const string ORIGINAL_URL = "https://www.youtube.com/";
        private Domain.Entities.Url url1 = new Domain.Entities.Url(ORIGINAL_URL);
        private Domain.Entities.Url url2 = new Domain.Entities.Url("https://www.google.com/");

        private Task<IEnumerable<Domain.Entities.Url>> GetUrlsAsync()
        {
            return Task<IEnumerable<Domain.Entities.Url>>.Run(() => (new List<Domain.Entities.Url> { url2 }).AsEnumerable());
        }

        private Task<Domain.Entities.Url> GetUrlAsync()
        {
            return Task<Domain.Entities.Url>.Run(() => url1);
        }

        [SetUp]
        public void Setup()
        {
            _urlRepository = new Mock<IUrlRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _urlRepository.Setup(x => x.CreateAsync(It.IsAny<Domain.Entities.Url>()))
                          .Returns(Task<Domain.Entities.Url>.Factory.StartNew(() => url1));
            _urlRepository.Setup(x => x.GetAsync())
                          .Returns(Task<IEnumerable<Domain.Entities.Url>>.Factory.StartNew(() => (new List<Domain.Entities.Url> { url2 }).AsEnumerable()));
        }

        [Test]
        public void CreateUrlCommandHandler_Instance()
        {
            // Arrange
            CreateUrlCommand command = new CreateUrlCommand
            {
                UrlDto = new DTOs.UrlDto
                {
                    OriginalUrl = ORIGINAL_URL
                }
            };
            CreateUrlCommandHandler handler = new CreateUrlCommandHandler(_urlRepository.Object, _unitOfWork.Object);

            // Act
            Guid urlId = handler.Handle(command, new CancellationToken()).Result;

            // Asserts
            Assert.AreNotEqual(Guid.Empty, urlId);
            _urlRepository.Verify(x => x.CreateAsync(It.IsAny<Domain.Entities.Url>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}

