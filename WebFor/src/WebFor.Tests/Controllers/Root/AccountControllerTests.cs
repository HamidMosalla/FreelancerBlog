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
using WebFor.Tests.HandMadeFakes;
using WebFor.Web.ViewModels.Account;
using WebFor.Core.Repository;
using WebFor.Core.Services.Shared;

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
        private Mock<IEmailSender> _emailSender;
        private Mock<ISmsSender> _smsSender;
        private Mock<ILoggerFactoryWrapper> _loggerFactoryWrapper;
        private Mock<ILogger<AccountController>> _logger;
        private Mock<IConfiguration> _configuration;
        private Mock<IUrlHelper> _urlHelper;
        private Mock<IRazorViewToString> _razorViewToString;
        private Mock<IHttpContextAccessor> _httpContextAccessor;

        public AccountControllerTests()
        {
             _httpContextAccessor = new Mock<IHttpContextAccessor>();

            _logger = new Mock<ILogger<AccountController>>();
            _loggerFactoryWrapper = new Mock<ILoggerFactoryWrapper>();
            _loggerFactoryWrapper.Setup(l => l.CreateLogger<AccountController>()).Returns(_logger.Object);

            _razorViewToString = new Mock<IRazorViewToString>();
            _configuration = new Mock<IConfiguration>();
            _smsSender = new Mock<ISmsSender>();
            _emailSender = new Mock<IEmailSender>();
            _uw = new Mock<IUnitOfWork>();
            _contactRepository = new Mock<IContactRepository>();
            _webForMapper = new Mock<IWebForMapper>();
            _captchaValidator = new Mock<ICaptchaValidator>();
            _configurationWrapper = new Mock<IConfigurationBinderWrapper>();
            var httpContext = new Mock<HttpContext>();
            _urlHelper = new Mock<IUrlHelper>();
            var tempDataProvider = new Mock<ITempDataProvider>();
            _tempData = new Mock<TempDataDictionary>(httpContext.Object, tempDataProvider.Object);

        }


        [Fact]
        public void Login_ShouldReturnDefaultView_WhenNoParameterSupplied()
        {
            var userManager = new UserManagerFake(isUserConfirmed: false);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            //Act
            var result = (ViewResult)sut.Login(returnUrl: null);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
        }

        [Fact]
        public void LoginPost_ShouldReturnFilledReturnUrlViewData_WhenNoParameterSupplied()
        {
            var userManager = new UserManagerFake(isUserConfirmed: false);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            //Act
            var result = (ViewResult)sut.Login(returnUrl: "/");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewData.Should().NotBeNull();
            result.ViewData.Should().NotBeEmpty();
            result.ViewData.ContainsKey("ReturnUrl").Should().BeTrue();
            result.ViewData["ReturnUrl"].Should().Be("/");
        }

        [Fact]
        public async Task LoginPost_ShouldReturnDefaultView_IfModelStateIsNotValid()
        {
            var userManager = new UserManagerFake(isUserConfirmed: false);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
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
        public async Task LoginPost_ShouldReturnDefaultViewWithModel_IfModelStateIsNotValid()
        {
            var userManager = new UserManagerFake(isUserConfirmed: false);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
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
        public async Task LoginPost_ShouldReturnDefaultView_IfUserIsNotConfirmed()
        {
            var userManager = new UserManagerFake(isUserConfirmed: false);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            //Act
            var result = (ViewResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "/");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task LoginPost_ShouldReturnDefaultViewWithModel_IfUserIsNotConfirmed()
        {
            var userManager = new UserManagerFake(isUserConfirmed: false);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            //Act
            var result = (ViewResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "/");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<LoginViewModel>();
        }

        [Fact]
        public async Task LoginPost_ShouldReturnDefaultViewWithProperViewData_IfUserIsNotConfirmed()
        {
            var userManager = new UserManagerFake(isUserConfirmed: false);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            //Act
            var result = (ViewResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "/");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewData.Should().NotBeNull();
            result.ViewData.Should().NotBeEmpty();
            result.ViewData.ContainsKey("LoginMessage").Should().BeTrue();
            result.ViewData["LoginMessage"].Should().Be("EmailIsNotConfirmed");
        }

        [Fact]
        public async Task LoginPost_ShouldRedirectToReturnUrl_IfSignInResultIsSucceeded()
        {
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            _urlHelper.Setup(u => u.IsLocalUrl(It.IsAny<string>())).Returns(true);

            sut.Url = _urlHelper.Object;

            //Act
            var result = (RedirectResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "/Home/About");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectResult>();
            result.Url.Should().NotBeNull();
            result.Url.Should().Be("/Home/About");
        }

        [Fact]
        public async Task LoginPost_ShouldRedirectToHomeIndex_IfSignInResultIsSucceededAndUrlNotLocal()
        {
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            _urlHelper.Setup(u => u.IsLocalUrl(It.IsAny<string>())).Returns(false);

            sut.Url = _urlHelper.Object;

            //Act
            var result = (RedirectToActionResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "http://google.com");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToActionResult>();
            result.ControllerName.Should().NotBeNull();
            result.ActionName.Should().NotBeNull();
            result.ControllerName.Should().Be("Home");
            result.ActionName.Should().Be("Index");
        }


        [Fact]
        public async Task LoginPost_ShouldRedirectToSendCodeAction_IfSignInResultIsRequiresTwoFactor()
        {
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.TwoFactorRequired);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            _urlHelper.Setup(u => u.IsLocalUrl(It.IsAny<string>())).Returns(false);

            sut.Url = _urlHelper.Object;

            //Act
            var result = (RedirectToActionResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "http://google.com");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToActionResult>();
            result.ActionName.Should().NotBeNull();
            result.ActionName.Should().Be("SendCode");
        }

        [Fact]
        public async Task LoginPost_ShouldReturnLockOutView_IfSignInResultIsLockedOut()
        {
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.LockedOut);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            _urlHelper.Setup(u => u.IsLocalUrl(It.IsAny<string>())).Returns(false);

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ViewResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "http://google.com");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().Be("Lockout");
        }

        [Fact]
        public async Task LoginPost_ShouldReturnDefaultView_IfSignInResultDidNotMatchAnyIf()
        {
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            _urlHelper.Setup(u => u.IsLocalUrl(It.IsAny<string>())).Returns(false);

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ViewResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "http://google.com");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            sut.ModelState.IsValid.Should().BeFalse();
            result.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task LoginPost_ShouldReturnDefaultViewWithModel_IfSignInResultDidNotMatchAnyIf()
        {
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _smsSender.Object, _loggerFactoryWrapper.Object, _captchaValidator.Object, _configuration.Object,
                _razorViewToString.Object);

            _urlHelper.Setup(u => u.IsLocalUrl(It.IsAny<string>())).Returns(false);

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ViewResult)await sut.Login(A.New<LoginViewModel>(), returnUrl: "http://google.com");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.Model.Should().BeOfType<LoginViewModel>();
            result.Model.Should().NotBeNull();
        }

    }
}
