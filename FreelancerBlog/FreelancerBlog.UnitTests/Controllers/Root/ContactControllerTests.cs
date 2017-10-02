using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
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
        private readonly IMediator _mediatorFake;
        private readonly IMapper _mapperFake;
        private readonly ContactController _sut;

        private TempDataDictionary _tempDataFake;

        public ContactControllerTests()
        {
            _mediatorFake = A.Fake<IMediator>();
            _mapperFake = A.Fake<IMapper>();
            var httpContext = A.Fake<HttpContext>();
            var tempDataProvider = A.Fake<ITempDataProvider>();
            _tempDataFake = A.Fake<TempDataDictionary>(t => t.WithArgumentsForConstructor(new object[] { httpContext, tempDataProvider }));

            _sut = new ContactController(_mapperFake, _mediatorFake);
        }

        [Fact]
        [Trait("Category", "DefaultView")]
        void CreateGet_Always_ReturnsCorrectView()
        {
            //act
            var result = (ViewResult)_sut.Create();

            //assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal("Create", result.ViewName);
        }

        [Fact]
        public async Task CreatePost_JavaScriptDisabled_ReturnsContactView()
        {
            var captchaResponse = new CaptchaResponse { Success = "true" };
            A.CallTo(() => _mediatorFake.Send(A<ValidateCaptchaQuery>._, A<CancellationToken>._)).Returns(captchaResponse);
            A.CallTo(() => _mediatorFake.Send(A<AddNewContactCommand>._, A<CancellationToken>._)).Returns(Task.CompletedTask);
            var viewModel = new ContactViewModel { ContactId = 1 };


            var result = (ViewResult)await _sut.Create(viewModel, false);


            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().Be("Success");
        }

        [Fact]
        public async Task CreatePost_ModelStateValid_PassesTheCorrectViewModelToMap()
        {
            var captchaResponse = new CaptchaResponse { Success = "true" };
            A.CallTo(() => _mediatorFake.Send(A<ValidateCaptchaQuery>._, A<CancellationToken>._)).Returns(captchaResponse);
            A.CallTo(() => _mediatorFake.Send(A<AddNewContactCommand>._, A<CancellationToken>._)).Returns(Task.CompletedTask);
            var viewModel = new ContactViewModel { ContactId = 1 };


            var result = (ViewResult)await _sut.Create(viewModel, false);

            A.CallTo(() => _mapperFake.Map<ContactViewModel, Contact>(A<ContactViewModel>.That.Matches(c => c == viewModel))).MustHaveHappened();
        }

        [Fact]
        public async Task CreatePost_ModelStateValid_PassesTheCorrectContactToAddNewContactCommand()
        {
            var captchaResponse = new CaptchaResponse { Success = "true" };
            var contact = new Contact { ContactId = 1 };
            A.CallTo(() => _mediatorFake.Send(A<ValidateCaptchaQuery>._, A<CancellationToken>._)).Returns(captchaResponse);
            A.CallTo(() => _mapperFake.Map<ContactViewModel, Contact>(A<ContactViewModel>._)).Returns(contact);

            var result = (ViewResult)await _sut.Create(It.IsAny<ContactViewModel>(), false);

            A.CallTo(() => _mediatorFake.Send(A<AddNewContactCommand>.That.Matches(c => c.Contact == contact), A<CancellationToken>._)).MustHaveHappened();
        }

        [Fact]
        public async Task CreatePost_ModelStateIsNotValid_ReturnsView()
        {
            var captchaResponse = new CaptchaResponse { Success = "true" };
            var viewModel = new ContactViewModel { ContactId = 1 };
            A.CallTo(() => _mediatorFake.Send(A<ValidateCaptchaQuery>._, A<CancellationToken>._)).Returns(captchaResponse);


            _sut.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            var result = (ViewResult)await _sut.Create(viewModel, false);

            result.ViewName.Should().BeNull();
            result.ViewData.Model.Should().BeOfType<ContactViewModel>();
            result.ViewData.Model.Should().NotBeNull();
            result.ViewData.Model.Should().Be(viewModel);
        }

        [Fact]
        public async Task CreatePost_JavaScriptEnabled_ReturnsJsonSuccess()
        {
            var captchaResponse = new CaptchaResponse { Success = "true" };
            var contact = new Contact { ContactId = 1 };
            var viewModel = new ContactViewModel { ContactId = 1 };
            A.CallTo(() => _mediatorFake.Send(A<ValidateCaptchaQuery>._, A<CancellationToken>._)).Returns(captchaResponse);
            A.CallTo(() => _mapperFake.Map<ContactViewModel, Contact>(A<ContactViewModel>._)).Returns(contact);

            var result = (JsonResult)await _sut.Create(viewModel, true);

            result.Should().NotBeNull();
            result.Should().BeOfType<JsonResult>();
            result.Value.Should().NotBeNull();
            result.Value.GetType().GetProperty("Status").GetValue(result.Value).Should().Be("Success");
        }

        [Fact]
        public async Task CreatePost_CaptchaValidationFailed_ReturnsJsonFailure()
        {
            var captchaResponse = new CaptchaResponse { Success = "false" };
            A.CallTo(() => _mediatorFake.Send(A<ValidateCaptchaQuery>._, A<CancellationToken>._)).Returns(captchaResponse);

            var result = (JsonResult)await _sut.Create(It.IsAny<ContactViewModel>(), true);

            result.Should().NotBeNull();
            result.Should().BeOfType<JsonResult>();
            result.Value.Should().NotBeNull();
            result.Value.GetType().GetProperty("status").GetValue(result.Value).Should().Be("FailedTheCaptchaValidation");
        }
    }
}
