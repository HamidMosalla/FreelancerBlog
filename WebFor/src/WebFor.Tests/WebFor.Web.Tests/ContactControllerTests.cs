using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Moq;
using WebFor.Web.Mapper;
using WebFor.Core.Repository;
using WebFor.Core.Services.Shared;
using WebFor.Web.Controllers;
using WebFor.Web.ViewModels.Contact;
using Xunit;
using WebFor.Core.Types;
using WebFor.Infrastructure.Services.Shared;
using System.Reflection;
using FluentAssertions;
using GenFu;

namespace WebFor.Tests.WebFor.Web.Tests
{
    public class ContactControllerTests
    {
        private Mock<IUnitOfWork> _uw;
        private Mock<IWebForMapper> _webForMapper;
        private Mock<ICaptchaValidator> _captchaValidator;
        private Mock<IConfigurationBinderWrapper> _configurationWrapper;
        private Mock<HttpContext> _httpContext;
        private Mock<ITempDataProvider> _tempDataProvider;
        private Mock<TempDataDictionary> _tempData;


        public ContactControllerTests()
        {
            _uw = new Mock<IUnitOfWork>();
            _webForMapper = new Mock<IWebForMapper>();
            _captchaValidator = new Mock<ICaptchaValidator>();
            _configurationWrapper = new Mock<IConfigurationBinderWrapper>();
            _httpContext = new Mock<HttpContext>();
            _tempDataProvider = new Mock<ITempDataProvider>();
            _tempData = new Mock<TempDataDictionary>(_httpContext.Object, _tempDataProvider.Object);

        }

        [Fact]
        void Create_SouldReturn_NotNull()
        {
            //arrange
            var sut = new ContactController(_uw.Object, _webForMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            //act
            var result = (ViewResult)sut.Create();

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        void Create_SouldReturn_IndexView()
        {
            var sut = new ContactController(_uw.Object, _webForMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            var result = (ViewResult)sut.Create();

            Assert.Equal("Create", result.ViewName);
        }

        [Fact]
        void Create_SouldReturn_TheSameType()
        {
            var sut = new ContactController(_uw.Object, _webForMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            var result = (ViewResult)sut.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_SouldReturn_ContactViewModel()
        {
            _captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
                .ReturnsAsync(new CaptchaResponse
                {
                    ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                    ErrorCodes = new List<string> { "404" },
                    HostName = "localhost",
                    Success = "true"
                });

            _uw.Setup(u => u.ContactRepository.AddNewContactAsync(new Core.Domain.Contact { })).ReturnsAsync(0);

            var sut = new ContactController(_uw.Object, _webForMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            sut.TempData = _tempData.Object;

            var result = (ViewResult)await sut.Create(new ContactViewModel(), false);

            Assert.IsType<ContactViewModel>(result.Model);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_SouldReturnFailedTheCaptchaValidation_IfCaptchaIsInvalid()
        {
            _captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
                .ReturnsAsync(new CaptchaResponse
                {
                    ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                    ErrorCodes = new List<string> { "404" },
                    HostName = "localhost",
                    Success = "false"
                });

            var sut = new ContactController(_uw.Object, _webForMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            var result = (JsonResult)await sut.Create(new ContactViewModel(), false);

            result.Value.GetType()
                .GetProperty("status")
                .GetValue(result.Value)
                .Should()
                .Be("FailedTheCaptchaValidation");
        }

        [Fact]
        public async Task Create_SouldReturnViewWithModel_IfModelStateIsNotValid()
        {
            //Arrange
            _captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
                .ReturnsAsync(new CaptchaResponse
                {
                    ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                    ErrorCodes = new List<string> {},
                    HostName = "localhost",
                    Success = "true"
                });

            _uw.Setup(u => u.ContactRepository.AddNewContactAsync(new Core.Domain.Contact { })).ReturnsAsync(0);

            var sut = new ContactController(_uw.Object, _webForMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            sut.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            var contactViewModel = new ContactViewModel();


            //Act
            var result = (ViewResult)await sut.Create(contactViewModel, false);
            

            //Assert
            result.ViewData.Model.Should().BeOfType<ContactViewModel>();
            result.ViewData.Model.Should().NotBeNull();

        }

    }
}
