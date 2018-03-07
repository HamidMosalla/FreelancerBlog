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
        private readonly IHttpContextAccessor _contextAccessorFake;
        private readonly FakeHttpMessageHandler _httpMessageHandlerFake;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configurationFake;
        private readonly ValidateCaptchaQueryHandler _sut;

        public ValidateCaptchaQueryHandlerShould()
        {
            _contextAccessorFake = A.Fake<IHttpContextAccessor>();
            //new Mock<FakeHttpMessageHandler> { CallBase = true };
            _httpMessageHandlerFake = A.Fake<FakeHttpMessageHandler>(a => a.CallsBaseMethods());
            _httpClient = new HttpClient(_httpMessageHandlerFake);
            _configurationFake = A.Fake<IConfiguration>();
            _sut = new ValidateCaptchaQueryHandler(_configurationFake, _contextAccessorFake, _httpClient);
        }

        [Fact]
        public async Task ValidateCaptchaAsync_Always_TheCorrectType()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var captchaQuery = new ValidateCaptchaQuery();

            A.CallTo(() => _contextAccessorFake.HttpContext).Returns(httpContext);
            A.CallTo(() => _contextAccessorFake.HttpContext.Request.Form).Returns(new FormCollection(new Dictionary<string, StringValues>()));

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




        //[Fact]
        //public async Task ValidateCaptchaAsync_ShouldReturnSuccessFalse_IfResponseSuccessWasFalse()
        //{
        //    //Arrange
        //    var httpContext = new DefaultHttpContext();

        //    _contextAccessor.SetupGet(c => c.HttpContext).Returns(httpContext);
        //    _contextAccessor.SetupGet(c => c.HttpContext.Request.Form)
        //        .Returns(new FormCollection(new Dictionary<string, StringValues>()));

        //    _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
        //    {
        //        StatusCode = HttpStatusCode.OK,
        //        Content =
        //            new StringContent(
        //                "{\"success\": false,\"error-codes\": [\"It's a fake error!\",\"It's a fake error\"]}")
        //    });

        //    var sut = new CaptchaValidator(_contextAccessor.Object, _httpClient);

        //    //Act
        //    var result = await sut.ValidateCaptchaAsync("dummy-secret");

        //    //Assert
        //    result.Should().NotBeNull();
        //    result.Success.Should().Be("false");
        //}


        //[Fact]
        //public async Task ValidateCaptchaAsync_ShouldReturnSuccessTrue_IfResponseSuccessWasTrue()
        //{
        //    //Arrange
        //    var httpContext = new DefaultHttpContext();

        //    _contextAccessor.SetupGet(c => c.HttpContext).Returns(httpContext);
        //    _contextAccessor.SetupGet(c => c.HttpContext.Request.Form)
        //        .Returns(new FormCollection(new Dictionary<string, StringValues>()));

        //    _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
        //    {
        //        StatusCode = HttpStatusCode.OK,
        //        Content =
        //            new StringContent(
        //                "{\"success\": true,\"error-codes\": [\"It's a fake error!\",\"It's a fake error\"]}")
        //    });

        //    var sut = new CaptchaValidator(_contextAccessor.Object, _httpClient);

        //    //Act
        //    var result = await sut.ValidateCaptchaAsync("dummy-secret");

        //    //Assert
        //    result.Should().NotBeNull();
        //    result.Success.Should().Be("true");
        //}


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


////_captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
////.ReturnsAsync(new CaptchaResponse
////{
////ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
////ErrorCodes = new List<string> { },
////HostName = "localhost",
////Success = "true"
////});