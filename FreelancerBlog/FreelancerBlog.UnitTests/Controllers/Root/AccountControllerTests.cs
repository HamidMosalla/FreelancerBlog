using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Controllers;
using FreelancerBlog.Core.Queries.Services.Shared;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Core.Types;
using FreelancerBlog.Core.Wrappers;
using FreelancerBlog.UnitTests.HandMadeFakes;
using FreelancerBlog.ViewModels.Account;
using FreelancerBlog.ViewModels.Email;
using GenFu;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FreelancerBlog.UnitTests.Controllers.Root
{
    public class AccountControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<HttpContext> _httpContext;
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
            _mediatorMock = new Mock<IMediator>();
            new Mock<IMapper>();
            new Mock<IConfigurationBinderWrapper>();
            _httpContext = new Mock<HttpContext>();
            _urlHelper = new Mock<IUrlHelper>();
            var tempDataProvider = new Mock<ITempDataProvider>();
            _tempData = new Mock<TempDataDictionary>(_httpContext.Object, tempDataProvider.Object);

        }


        [Fact]
        public void Login_ShouldReturnDefaultView_WhenNoParameterSupplied()
        {
            var userManager = new UserManagerFake(isUserConfirmed: false);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

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
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

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
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

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
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

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
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

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
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

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
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

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
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

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
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

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
        public async Task LoginPost_ShouldReturnLockOutView_IfSignInResultIsLockedOut()
        {
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.LockedOut);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

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
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

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
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

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


        [Fact]
        void Register_ShouldReturnDefaultView_AllTheTime()
        {
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Arrange
            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

            _urlHelper.Setup(u => u.IsLocalUrl(It.IsAny<string>())).Returns(false);

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ViewResult)sut.Register();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task Register_ShouldReturnRegisterView_IfCapthchaFailed()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var captchaResponse = new CaptchaResponse { Success = "false" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);


            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

            //Act
            var result = (ViewResult)await sut.Register(A.New<RegisterViewModel>());

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
        }


        [Fact]
        public async Task Register_ShouldReturnRegisterViewWithModel_IfCapthchaFailed()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var captchaResponse = new CaptchaResponse { Success = "false" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);


            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

            //Act
            var result = (ViewResult)await sut.Register(A.New<RegisterViewModel>());

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<RegisterViewModel>();
        }


        [Fact]
        public async Task Register_ShouldReturnRegisterViewWithViewData_IfCapthchaFailed()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var captchaResponse = new CaptchaResponse { Success = "false" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);


            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

            //Act
            var result = (ViewResult)await sut.Register(A.New<RegisterViewModel>());

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewData.Should().NotBeNull();
            result.ViewData.ContainsKey("FailedTheCaptchaValidation").Should().BeTrue();
            result.ViewData["FailedTheCaptchaValidation"].Should().Be("true");
        }


        [Fact]
        public async Task Register_ShouldReturnRegisterView_IfModelValidationFailed()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var captchaResponse = new CaptchaResponse { Success = "true" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);


            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

            sut.ModelState.AddModelError(string.Empty, "Dummy Error");

            //Act
            var result = (ViewResult)await sut.Register(A.New<RegisterViewModel>());

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
        }



        [Fact]
        public async Task Register_ShouldReturnRegisterViewWithModel_IfModelValidationFailed()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var captchaResponse = new CaptchaResponse { Success = "true" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);

            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

            sut.ModelState.AddModelError(string.Empty, "Dummy Error");

            //Act
            var result = (ViewResult)await sut.Register(A.New<RegisterViewModel>());

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<RegisterViewModel>();
        }



        [Fact]
        public async Task Register_ShouldReturnRegisterView_IfCreateAsyncIdentityResultReturnFailed()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Failed());
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var captchaResponse = new CaptchaResponse { Success = "true" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);


            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

            //Act
            var result = (ViewResult)await sut.Register(A.New<RegisterViewModel>());

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
        }


        [Fact]
        public async Task Register_ShouldReturnRegisterViewWithModel_IfCreateAsyncIdentityResultReturnFailed()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Failed());
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var captchaResponse = new CaptchaResponse { Success = "true" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);


            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

            //Act
            var result = (ViewResult)await sut.Register(A.New<RegisterViewModel>());

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<RegisterViewModel>();
        }


        [Fact]
        public async Task Register_ShouldReturnInfoMessageView_IfCreateAsyncIdentityResultReturnSuccess()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var captchaResponse = new CaptchaResponse { Success = "true" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);


            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);


            sut.ControllerContext.HttpContext = new DefaultHttpContext();

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ViewResult)await sut.Register(A.New<RegisterViewModel>());

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().Be("InfoMessage");
        }

        [Fact]
        public async Task Register_ShouldReturnInfoMessageViewWithFilledViewBag_IfCreateAsyncIdentityResultReturnSuccess()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var captchaResponse = new CaptchaResponse { Success = "true" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);


            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);


            sut.ControllerContext.HttpContext = new DefaultHttpContext();

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ViewResult)await sut.Register(A.New<RegisterViewModel>());

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.Should().NotBeNull();
            result.ViewData.Should().NotBeEmpty();
            result.ViewData.ContainsKey("InfoMessage").Should().BeTrue();
            result.ViewData["InfoMessage"].Should().Be("RegistrationConfirmEmail");
        }

        [Fact]
        public async Task Register_ShouldCallRazorViewToString_ExactlyOnce()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);
            var captchaResponse = new CaptchaResponse {Success = "true"};
            _mediatorMock.Setup(m=>m.Send(It.IsAny< ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);

            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);


            sut.ControllerContext.HttpContext = new DefaultHttpContext();

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ViewResult)await sut.Register(A.New<RegisterViewModel>());

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            _razorViewToString.Verify(r => r.Render(It.IsAny<string>(), It.IsAny<IdentityTemplateViewModel>()), Times.Once);
        }

        [Fact]
        public async Task Register_ShouldCallSendEmailAsync_ExactlyOnce()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var captchaResponse = new CaptchaResponse { Success = "true" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);

            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);


            sut.ControllerContext.HttpContext = new DefaultHttpContext();

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ViewResult)await sut.Register(A.New<RegisterViewModel>());

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            _emailSender.Verify(e => e.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task LogOff_ShouldRedirectTo_HomeIndex()
        {
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

            var result = (RedirectToActionResult)await sut.LogOff();

            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToActionResult>();
            result.ControllerName.Should().Be("Home");
            result.ActionName.Should().Be(nameof(HomeController.Index));
        }

        [Fact]
        public void ExternalLogin_ShouldReturnANew_ChallengeResult()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, signInResult: Microsoft.AspNetCore.Identity.SignInResult.Failed);


            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ChallengeResult)sut.ExternalLogin("google");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ChallengeResult>();
            result.Properties.Should().NotBeNull();
        }

        [Fact]
        public async Task ExternalLoginCallback_ShouldRedirectToLogin_IfExternalLoginInfoIsNull()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, Microsoft.AspNetCore.Identity.SignInResult.Failed, null);


            var sut = new AccountController(userManager, signInManager, _emailSender.Object, _loggerFactoryWrapper.Object, _razorViewToString.Object, _mediatorMock.Object);

            sut.ControllerContext.HttpContext = new DefaultHttpContext();

            //Act
            var result = (RedirectToActionResult)await sut.ExternalLoginCallback();


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToActionResult>();
            result.ActionName.Should().Be(nameof(AccountController.Login));
        }

        [Fact]
        public async Task ExternalLoginCallback_ShouldRedirectToReturnUrl_IfExternalLoginSignInAsyncResultIsSuccess()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, Microsoft.AspNetCore.Identity.SignInResult.Success, new ExternalLoginInfo(new System.Security.Claims.ClaimsPrincipal(), string.Empty, string.Empty, string.Empty));


            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                _loggerFactoryWrapper.Object, _razorViewToString.Object,
                _mediatorMock.Object) {Url = _urlHelper.Object};


            //Act
            var result = (RedirectToActionResult)await sut.ExternalLoginCallback("/Home/Index");


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToActionResult>();
            result.ControllerName.Should().Be("Home");
            result.ActionName.Should().Be(nameof(HomeController.Index));
        }

        [Fact]
        public async Task ExternalLoginCallback_ShouldReturnLockoutView_IfExternalLoginSignInAsyncResultIsLockedOut()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, Microsoft.AspNetCore.Identity.SignInResult.LockedOut, new ExternalLoginInfo(new System.Security.Claims.ClaimsPrincipal(), string.Empty, string.Empty, string.Empty));


            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                    _loggerFactoryWrapper.Object, _razorViewToString.Object,
                    _mediatorMock.Object)
                { Url = _urlHelper.Object };

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ViewResult)await sut.ExternalLoginCallback("/Home/Index");


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().Be("Lockout");
        }

        [Fact]
        public async Task ExternalLoginCallback_ShouldReturnExternalLoginConfirmationView_IfUserDoesNotHaveAnAccount()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, Microsoft.AspNetCore.Identity.SignInResult.Failed, new ExternalLoginInfo(new System.Security.Claims.ClaimsPrincipal(), string.Empty, string.Empty, string.Empty));


            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                    _loggerFactoryWrapper.Object, _razorViewToString.Object,
                    _mediatorMock.Object)
                { Url = _urlHelper.Object };

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ViewResult)await sut.ExternalLoginCallback("/Home/Index");


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().Be("ExternalLoginConfirmation");
        }

        [Fact]
        public async Task ExternalLoginCallback_ShouldReturnExternalLoginConfirmationViewWithModel_IfUserDoesNotHaveAnAccount()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, Microsoft.AspNetCore.Identity.SignInResult.Failed, new ExternalLoginInfo(new System.Security.Claims.ClaimsPrincipal(), string.Empty, string.Empty, string.Empty));


            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                    _loggerFactoryWrapper.Object, _razorViewToString.Object,
                    _mediatorMock.Object)
                { Url = _urlHelper.Object };

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ViewResult)await sut.ExternalLoginCallback("/Home/Index");


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<ExternalLoginConfirmationViewModel>();
        }

        [Fact]
        public async Task ExternalLoginCallback_ShouldReturnExternalLoginConfirmationViewWithFilledViewData_IfUserDoesNotHaveAnAccount()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(_httpContextAccessor.Object, Microsoft.AspNetCore.Identity.SignInResult.Failed, new ExternalLoginInfo(new System.Security.Claims.ClaimsPrincipal(), string.Empty, string.Empty, string.Empty));


            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                    _loggerFactoryWrapper.Object, _razorViewToString.Object,
                    _mediatorMock.Object)
                { Url = _urlHelper.Object };

            sut.Url = _urlHelper.Object;

            //Act
            var result = (ViewResult)await sut.ExternalLoginCallback("/Home/Index");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewData.ContainsKey("ReturnUrl").Should().BeTrue();
            result.ViewData.ContainsKey("LoginProvider").Should().BeTrue();
            result.ViewData["ReturnUrl"].Should().Be("/Home/Index");
            result.ViewData["LoginProvider"].Should().Be(signInManager.GetExternalLoginInfoAsync().Result.LoginProvider);
        }

        [Fact]
        public async Task ExternalLoginConfirmation_ShouldRedirectToActionIndexOfManageController_IfTheUserIsSignedIn()
        {
            //Arrange
            var userManager = new UserManagerFake(isUserConfirmed: true, identityResult: IdentityResult.Success);
            var signInManager = new SignInManagerFake(
                _httpContextAccessor.Object,
                Microsoft.AspNetCore.Identity.SignInResult.Failed,
                new ExternalLoginInfo(new System.Security.Claims.ClaimsPrincipal(), string.Empty, string.Empty, string.Empty),
                isSignIn:true);

            var sut = new AccountController(userManager, signInManager, _emailSender.Object,
                    _loggerFactoryWrapper.Object, _razorViewToString.Object,
                    _mediatorMock.Object)
                { Url = _urlHelper.Object };

            //Act
            var result = (RedirectToActionResult) await sut.ExternalLoginConfirmation(It.IsAny<ExternalLoginConfirmationViewModel>());

            result.Should().BeOfType<RedirectToActionResult>();
            result.ControllerName.Should().Be("Manage");
            result.ActionName.Should().Be("Index");
        }

    }
}
