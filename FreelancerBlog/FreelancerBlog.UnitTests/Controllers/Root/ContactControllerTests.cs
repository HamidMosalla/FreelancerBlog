using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Controllers;
using FreelancerBlog.Core.Commands.Data.Contacts;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Services.Shared;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Core.Types;
using FreelancerBlog.Core.Wrappers;
using FreelancerBlog.ViewModels.Contact;
using GenFu;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;

namespace FreelancerBlog.UnitTests.Controllers.Root
{
    public class ContactControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IMapper> _mapperMock;

        private Mock<TempDataDictionary> _tempData;


        public ContactControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            var httpContext = new Mock<HttpContext>();
            var tempDataProvider = new Mock<ITempDataProvider>();
            _tempData = new Mock<TempDataDictionary>(httpContext.Object, tempDataProvider.Object);

        }

        [Fact]
        [Trait("Category", "DefaultView")]
        void CreateGetReturnsCorrectView()
        {
            //arrange
            var sut = new ContactController(_mapperMock.Object, _mediatorMock.Object);

            //act
            var result = (ViewResult)sut.Create();

            //assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal("Create", result.ViewName);
        }

        [Fact]
        public async Task CreatePostReturnsContactViewWhenJavaScriptIsDisabled()
        {
            var captchaResponse = new CaptchaResponse { Success = "true" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddNewContactCommand>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            var viewModel = new ContactViewModel { ContactId = 1 };
            var sut = new ContactController(_mapperMock.Object, _mediatorMock.Object);

            var result = (ViewResult)await sut.Create(viewModel, false);

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().Be("Success");
        }

        [Fact]
        public async Task CreatePostMapReceivesTheCorrectViewModel()
        {
            var captchaResponse = new CaptchaResponse { Success = "true" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddNewContactCommand>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            var viewModel = new ContactViewModel { ContactId = 1 };
            var sut = new ContactController(_mapperMock.Object, _mediatorMock.Object);

            var result = (ViewResult)await sut.Create(viewModel, false);

            _mapperMock.Verify(m => m.Map<ContactViewModel, Contact>(It.Is<ContactViewModel>(c => c == viewModel)));
        }

        [Fact]
        public async Task CreatePostAddNewContactCommandReceivesTheCorrectContact()
        {
            var captchaResponse = new CaptchaResponse { Success = "true" };
            var contact = new Contact { ContactId = 1 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);
            _mapperMock.Setup(m => m.Map<ContactViewModel, Contact>(It.IsAny<ContactViewModel>())).Returns(contact);
            var sut = new ContactController(_mapperMock.Object, _mediatorMock.Object);

            var result = (ViewResult)await sut.Create(It.IsAny<ContactViewModel>(), false);

            _mediatorMock.Verify(m => m.Send(It.Is<AddNewContactCommand>(c => c.Contact == contact), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task CreatePostReturnsViewIdModelStateIsNotValid()
        {
            var captchaResponse = new CaptchaResponse { Success = "true" };
            var viewModel = new ContactViewModel { ContactId = 1 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);
            var sut = new ContactController(_mapperMock.Object, _mediatorMock.Object);
            sut.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            var result = (ViewResult)await sut.Create(viewModel, false);

            result.ViewName.Should().BeNull();
            result.ViewData.Model.Should().BeOfType<ContactViewModel>();
            result.ViewData.Model.Should().NotBeNull();
            result.ViewData.Model.Should().Be(viewModel);
        }

        [Fact]
        public async Task CreatePostReturnsJsonSuccessIfJavaScriptEnabled()
        {
            var captchaResponse = new CaptchaResponse { Success = "true" };
            var contact = new Contact { ContactId = 1 };
            var viewModel = new ContactViewModel { ContactId = 1 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);
            _mapperMock.Setup(m => m.Map<ContactViewModel, Contact>(It.IsAny<ContactViewModel>())).Returns(contact);
            var sut = new ContactController(_mapperMock.Object, _mediatorMock.Object);

            var result = (JsonResult)await sut.Create(viewModel, true);

            result.Should().NotBeNull();
            result.Should().BeOfType<JsonResult>();
            result.Value.Should().NotBeNull();
            result.Value.GetType().GetProperty("Status").GetValue(result.Value).Should().Be("Success");
        }

        [Fact]
        public async Task CreatePostReturnsJsonFailureIfCaptchaValidationFailed()
        {
            var captchaResponse = new CaptchaResponse { Success = "false" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCaptchaQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(captchaResponse);
            var sut = new ContactController(_mapperMock.Object, _mediatorMock.Object);

            var result = (JsonResult)await sut.Create(It.IsAny<ContactViewModel>(), true);

            result.Should().NotBeNull();
            result.Should().BeOfType<JsonResult>();
            result.Value.Should().NotBeNull();
            result.Value.GetType().GetProperty("status").GetValue(result.Value).Should().Be("FailedTheCaptchaValidation");
        }
    }
}
