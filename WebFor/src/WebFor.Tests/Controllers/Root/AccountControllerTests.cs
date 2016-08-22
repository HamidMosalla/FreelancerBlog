using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Moq;
using WebFor.Web.Mapper;
using WebFor.Web.Controllers;
using Xunit;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using GenFu;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebFor.Tests.Fixtures;
using WebFor.Core.Services.Shared;
using WebFor.Core.Repository;
using WebFor.Tests.HandMadeFakes;
using WebFor.Web.ViewModels.Account;

namespace WebFor.Tests.Controllers.Root
{
    public class AccountControllerTests
    {
        private Mock<IUnitOfWork> _uw;
        private Mock<IContactRepository> _contactRepository;
        private Mock<IWebForMapper> _webForMapper;
        private Mock<ICaptchaValidator> _captchaValidator;
        private Mock<IConfigurationBinderWrapper> _configurationWrapper;
        private Mock<TempDataDictionary> _tempData;
        private UserManagerFake _userManager;
        private SignInManagerFake _signInManager;
        private Mock<IEmailSender> _emailSender;
        private Mock<ISmsSender> _smsSender;
        private Mock<ILogger> _logger;
        private Mock<ILoggerFactory> _loggerFactory;
        private Mock<IConfiguration> _configuration;
        private Mock<IRazorViewToString> _razorViewToString;

        public AccountControllerTests()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();

            _loggerFactory = new Mock<ILoggerFactory>();
            _razorViewToString = new Mock<IRazorViewToString>();
            _configuration = new Mock<IConfiguration>();
            _logger = new Mock<ILogger>();
            _smsSender = new Mock<ISmsSender>();
            _emailSender = new Mock<IEmailSender>();
            _userManager = new UserManagerFake();
            _signInManager = new SignInManagerFake(httpContextAccessor.Object);
            _uw = new Mock<IUnitOfWork>();
            _contactRepository = new Mock<IContactRepository>();
            _webForMapper = new Mock<IWebForMapper>();
            _captchaValidator = new Mock<ICaptchaValidator>();
            _configurationWrapper = new Mock<IConfigurationBinderWrapper>();
            var httpContext = new Mock<HttpContext>();
            var tempDataProvider = new Mock<ITempDataProvider>();
            _tempData = new Mock<TempDataDictionary>(httpContext.Object, tempDataProvider.Object);

        }


        [Fact]
        public void Login_ShouldReturnDefaultView_WhenNoParameterSupplied()
        {

            //Arrange
            var sut = new AccountController(_userManager, _signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactory.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            //Act
            var result = (ViewResult)sut.Login(returnUrl: null);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
        }

        [Fact]
        public void Login_ShouldReturnFilledReturnUrlViewData_WhenNoParameterSupplied()
        {

            //Arrange
            var sut = new AccountController(_userManager, _signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactory.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            //Act
            var result = (ViewResult)sut.Login(returnUrl: "/");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewData.Should().NotBeNull();
        }

        [Fact]
        public async Task Login_ShouldReturnDefaultView_IfModelStateIsNotValid()
        {

            //Arrange
            var sut = new AccountController(_userManager, _signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactory.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            sut.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            //Act
            var result = (ViewResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "/");


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
            result.ViewData.Should().NotBeNull();
        }

        [Fact]
        public async Task Login_ShouldReturnDefaultViewWithModel_IfModelStateIsNotValid()
        {

            //Arrange
            var sut = new AccountController(_userManager, _signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactory.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            sut.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            //Act
            var result = (ViewResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "/");


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<LoginViewModel>();
        }

        [Fact]
        public async Task Login_ShouldReturnDefaultView_IfUserIsNotConfirmed()
        {
            //Arrange
            var sut = new AccountController(_userManager, _signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactory.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            sut.TempData = _tempData.Object;

            //Act
            var result = (ViewResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "/");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task Login_ShouldReturnDefaultViewWithModel_IfUserIsNotConfirmed()
        {
            //Arrange
            var sut = new AccountController(_userManager, _signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactory.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            sut.TempData = _tempData.Object;

            //Act
            var result = (ViewResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "/");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<LoginViewModel>();
        }

        [Fact]
        public async Task Login_ShouldReturnDefaultViewWithFilledTempData_IfUserIsNotConfirmed()
        {
            //Arrange
            var sut = new AccountController(_userManager, _signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactory.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            sut.TempData = _tempData.Object;

            //Act
            var result = (ViewResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "/");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.TempData.Should().NotBeNull();
            //result.TempData.Should().NotBeEmpty();
            //result.TempData.ContainsKey("LoginMessage").Should().BeTrue();
            //result.TempData["LoginMessage"].Should().Be("EmailIsNotConfirmed");
        }

    }
}
