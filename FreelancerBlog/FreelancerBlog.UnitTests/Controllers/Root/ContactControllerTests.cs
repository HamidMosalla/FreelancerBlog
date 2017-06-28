using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Controllers;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Core.Types;
using FreelancerBlog.Core.Wrappers;
using FreelancerBlog.Mapper;
using FreelancerBlog.ViewModels.Contact;
using GenFu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;

namespace FreelancerBlog.UnitTests.Controllers.Root
{
    public class ContactControllerTests
    {
        private Mock<IUnitOfWork> _uw;
        private Mock<IContactRepository> _contactRepository;
        private Mock<IFreelancerBlogMapper> _freelancerBlogMapper;
        private Mock<ICaptchaValidator> _captchaValidator;
        private Mock<IConfigurationBinderWrapper> _configurationWrapper;
        private Mock<TempDataDictionary> _tempData;


        public ContactControllerTests()
        {
            _uw = new Mock<IUnitOfWork>();
            _contactRepository = new Mock<IContactRepository>();
            _freelancerBlogMapper = new Mock<IFreelancerBlogMapper>();
            _captchaValidator = new Mock<ICaptchaValidator>();
            _configurationWrapper = new Mock<IConfigurationBinderWrapper>();
            var httpContext = new Mock<HttpContext>();
            var tempDataProvider = new Mock<ITempDataProvider>();
            _tempData = new Mock<TempDataDictionary>(httpContext.Object, tempDataProvider.Object);

        }

        [Fact]
        [Trait("Category", "DefaultView")]
        void Create_SouldReturn_CreateView()
        {
            //arrange
            var sut = new ContactController(_uw.Object, _freelancerBlogMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            //act
            var result = (ViewResult)sut.Create();

            //assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal("Create", result.ViewName);
        }

        [Fact]
        public async Task CreatePost_SouldReturnContactView_WhenJavaScriptIsDisabledAndThereIsNothingToSaveOrThereWasAProblem()
        {
            _captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
                .ReturnsAsync(new CaptchaResponse
                {
                    ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                    ErrorCodes = new List<string> { },
                    HostName = "localhost",
                    Success = "true"
                });

            _uw.Setup(u => u.ContactRepository.AddNewContactAsync(new Contact { })).ReturnsAsync(0);

            var sut = new ContactController(_uw.Object, _freelancerBlogMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            var result = (ViewResult)await sut.Create(new ContactViewModel(), false);


            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().Be("Create");
        }

        [Fact]
        public async Task CreatePost_SouldReturnContactViewWithProperViewData_WhenJavaScriptIsDisabledAndThereIsNothingToSaveOrThereWasAProblem()
        {
            _captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
                .ReturnsAsync(new CaptchaResponse
                {
                    ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                    ErrorCodes = new List<string> { },
                    HostName = "localhost",
                    Success = "true"
                });

            _uw.Setup(u => u.ContactRepository.AddNewContactAsync(new Contact { })).ReturnsAsync(0);

            var sut = new ContactController(_uw.Object, _freelancerBlogMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            var result = (ViewResult)await sut.Create(new ContactViewModel(), false);

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewData.Should().NotBeEmpty();
            result.ViewData.Should().ContainKey("CreateContactMessage");
            result.ViewData["CreateContactMessage"].Should().Be("NothingToSaveOrThereWasAProblem");
        }

        [Fact]
        public async Task CreatePost_SouldReturnContactViewModel_WhenJavaScriptIsDisabledAndThereIsNothingToSaveOrThereWasAProblem()
        {
            _captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
                .ReturnsAsync(new CaptchaResponse
                {
                    ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                    ErrorCodes = new List<string> { },
                    HostName = "localhost",
                    Success = "true"
                });

            _uw.Setup(u => u.ContactRepository.AddNewContactAsync(new Contact { })).ReturnsAsync(0);

            var sut = new ContactController(_uw.Object, _freelancerBlogMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            var result = (ViewResult)await sut.Create(new ContactViewModel(), false);

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewData.Model.Should().NotBeNull();
            result.ViewData.Model.Should().BeOfType<ContactViewModel>();
        }

        [Fact]
        public async Task CreatePost_SouldReturnFailedTheCaptchaValidation_IfCaptchaIsInvalid()
        {
            _captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
                .ReturnsAsync(new CaptchaResponse
                {
                    ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                    ErrorCodes = new List<string> { },
                    HostName = "localhost",
                    Success = "false"
                });

            var sut = new ContactController(_uw.Object, _freelancerBlogMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            var result = (JsonResult)await sut.Create(new ContactViewModel(), false);

            result.Should().NotBeNull();
            result.Should().BeOfType<JsonResult>();
            result.Value.GetType()
                .GetProperty("status")
                .GetValue(result.Value)
                .Should()
                .Be("FailedTheCaptchaValidation");
        }

        [Fact]
        public async Task CreatePost_SouldReturnViewWithModel_IfModelStateIsNotValid()
        {
            //Arrange
            _captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
                .ReturnsAsync(new CaptchaResponse
                {
                    ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                    ErrorCodes = new List<string> { },
                    HostName = "localhost",
                    Success = "true"
                });

            _uw.Setup(u => u.ContactRepository.AddNewContactAsync(new Contact { })).ReturnsAsync(0);

            var sut = new ContactController(_uw.Object, _freelancerBlogMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            sut.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            var contactViewModel = new ContactViewModel();


            //Act
            var result = (ViewResult)await sut.Create(contactViewModel, false);


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
            result.ViewData.Model.Should().BeOfType<ContactViewModel>();
            result.ViewData.Model.Should().NotBeNull();

        }

        [Fact]
        public async Task CreatePost_SouldReturnSuccessView_IfNewContactAdded()
        {
            _captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
                .ReturnsAsync(new CaptchaResponse
                {
                    ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                    ErrorCodes = new List<string> { },
                    HostName = "localhost",
                    Success = "true"
                });

            var contactViewModel = A.New<ContactViewModel>();

            _freelancerBlogMapper.Setup(s => s.ContactViewModelToContact(It.IsAny<ContactViewModel>())).Returns(A.New<Contact>());

            //It.IsAny<Contact>() means what ever passed in

            _contactRepository.Setup(c => c.AddNewContactAsync(It.IsAny<Contact>())).ReturnsAsync(10);

            _uw.SetupGet<IContactRepository>(u => u.ContactRepository).Returns(_contactRepository.Object);

            var sut = new ContactController(_uw.Object, _freelancerBlogMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            var result = (ViewResult)await sut.Create(contactViewModel, false);

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().Be("Success");
        }

        [Fact]
        public async Task CreatePost_SouldReturnSuccessJsonStatus_IfNewContactAdded()
        {
            _captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
                .ReturnsAsync(new CaptchaResponse
                {
                    ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                    ErrorCodes = new List<string> { },
                    HostName = "localhost",
                    Success = "true"
                });

            var contactViewModel = A.New<ContactViewModel>();

            _freelancerBlogMapper.Setup(s => s.ContactViewModelToContact(It.IsAny<ContactViewModel>())).Returns(A.New<Contact>());

            _contactRepository.Setup(c => c.AddNewContactAsync(It.IsAny<Contact>())).ReturnsAsync(10);

            _uw.SetupGet<IContactRepository>(u => u.ContactRepository).Returns(_contactRepository.Object);

            var sut = new ContactController(_uw.Object, _freelancerBlogMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            var result = (JsonResult)await sut.Create(contactViewModel, true);

            result.Should().NotBeNull();
            result.Should().BeOfType<JsonResult>();
            result.Value.Should().NotBeNull();
            result.Value.GetType().GetProperty("Status").GetValue(result.Value).Should().Be("Success");
        }

        [Fact]
        public async Task CreatePost_SouldReturnProblematicSubmitJsonStatus_IfThereWasAProblem()
        {
            _captchaValidator.Setup(c => c.ValidateCaptchaAsync(_configurationWrapper.Object.GetValue<string>("secrect")))
                .ReturnsAsync(new CaptchaResponse
                {
                    ChallengeTimeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                    ErrorCodes = new List<string> { },
                    HostName = "localhost",
                    Success = "true"
                });

            var contactViewModel = A.New<ContactViewModel>();

            _freelancerBlogMapper.Setup(s => s.ContactViewModelToContact(It.IsAny<ContactViewModel>())).Returns(A.New<Contact>());

            _contactRepository.Setup(c => c.AddNewContactAsync(It.IsAny<Contact>())).ReturnsAsync(0);

            _uw.SetupGet<IContactRepository>(u => u.ContactRepository).Returns(_contactRepository.Object);

            var sut = new ContactController(_uw.Object, _freelancerBlogMapper.Object, _captchaValidator.Object, _configurationWrapper.Object);

            var result = (JsonResult)await sut.Create(contactViewModel, true);

            result.Should().NotBeNull();
            result.Should().BeOfType<JsonResult>();
            result.Value.Should().NotBeNull();
            result.Value.GetType().GetProperty("Status").GetValue(result.Value).Should().Be("ProblematicSubmit");

            //pointless, here just for reference, use verify type of methods to test the operations
            //with unobservable1 behavior, for more info visit: http://stackoverflow.com/a/29509950/1650277
            //_contactRepository.VerifyAll();
            //_contactRepository.Verify(c => c.AddNewContactAsync(It.IsAny<Contact>()), Times.Once);
            //_freelancerBlogMapper.Verify(w => w.ContactViewModelToContact(It.IsAny<ContactViewModel>()), Times.Once);
        }
    }
}
