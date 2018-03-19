//using System.Collections.Generic;
//using System.Net;

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using FreelancerBlog.Core.Queries.Services.Shared;
using FreelancerBlog.Core.Types;
using FreelancerBlog.Services.Queries.Shared;
using FreelancerBlog.Services.Shared;
using FreelancerBlog.UnitTests.HandMadeFakes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace FreelancerBlog.UnitTests.Services.Shared
{
    public class ValidateCaptchaQueryHandlerShould
    {
        private readonly ValidateCaptchaQuery _validateCaptchaQuery = new ValidateCaptchaQuery();
        private readonly FakeHttpMessageHandler _httpMessageHandlerFake;
        private readonly ValidateCaptchaQueryHandler _sut;

        public ValidateCaptchaQueryHandlerShould()
        {
            var contextAccessorFake = A.Fake<IHttpContextAccessor>();
            _httpMessageHandlerFake = A.Fake<FakeHttpMessageHandler>(a => a.CallsBaseMethods());
            var httpClient = new HttpClient(_httpMessageHandlerFake);
            var configurationFake = A.Fake<IConfiguration>();
            _sut = new ValidateCaptchaQueryHandler(configurationFake, contextAccessorFake, httpClient);
        }

        [Fact]
        public async Task ValidateCaptchaAsync_Always_ReturnsTheCorrectType()
        {
            //Arrange
            var captchaQuery = new ValidateCaptchaQuery();

            A.CallTo(() => _httpMessageHandlerFake.Send(A<HttpRequestMessage>._)).Returns(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content =
                    new StringContent(
                        "{\"success\": false,\"error-codes\": [\"It's a fake error!\",\"It's a fake error\"]}")
            });

            //Act
            var result = await _sut.Handle(captchaQuery, default(CancellationToken));

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CaptchaResponse>();
        }

        [Fact]
        public async Task ValidateCaptchaAsync_ResponseSuccessFalse_ReturnSuccessFalse()
        {
            //Arrange
            A.CallTo(() => _httpMessageHandlerFake.Send(A<HttpRequestMessage>._)).Returns(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content =
                    new StringContent(
                        "{\"success\": false,\"error-codes\": [\"It's a fake error!\",\"It's a fake error\"]}")
            });

            //Act
            var result = await _sut.Handle(_validateCaptchaQuery, default(CancellationToken));

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().Be("false");
        }

        [Fact]
        public async Task ValidateCaptchaAsync_ResponseSuccessTrue_ReturnSuccessTrue()
        {
            //Arrange
            A.CallTo(() => _httpMessageHandlerFake.Send(A<HttpRequestMessage>._)).Returns(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content =
                    new StringContent(
                        "{\"success\": true,\"error-codes\": [\"It's a fake error!\",\"It's a fake error\"]}")
            });

            //Act
            var result = await _sut.Handle(_validateCaptchaQuery, default(CancellationToken));

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().Be("true");
        }
    }
}

//_captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
//.ReturnsAsync(new CaptchaResponse
//{
//ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
//ErrorCodes = new List<string> { },
//HostName = "localhost",
//Success = "true"
//});


//_captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
//.ReturnsAsync(new CaptchaResponse
//{
//ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
//ErrorCodes = new List<string> { },
//HostName = "localhost",
//Success = "true"
//});

//var httpContext = new DefaultHttpContext();
//A.CallTo(() => _contextAccessorFake.HttpContext).Returns(httpContext);
//A.CallTo(() => _contextAccessorFake.HttpContext.Request.Form)
//.Returns(new FormCollection(new Dictionary<string, StringValues>()));