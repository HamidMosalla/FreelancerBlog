using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using FreelancerBlog.Areas.Admin.Controllers;
using FreelancerBlog.Core.Commands.Data.Contacts;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Contacts;
using FreelancerBlog.UnitTests.Database;
using FreelancerBlog.UnitTests.Extensions;
using FreelancerBlog.ViewModels.Contact;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FreelancerBlog.UnitTests.Controllers.Admin
{
    public class ContactControllerTests : InMemoryContextTest
    {
        private IMediator _mediatorFake;
        private IMapper _mapperFake;
        private ContactController _sut;

        public ContactControllerTests()
        {
            _mediatorFake = A.Fake<IMediator>();
            _mapperFake = A.Fake<IMapper>();
            _sut = new ContactController(_mediatorFake, _mapperFake);
        }

        [Fact]
        public async Task ManageContact_Always_ReturnsTheCorrectType()
        {
            var result = await _sut.ManageContact() as ViewResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task ManageContact_Always_ReturnsTheCorrectViewModel()
        {
            var viewModels = new List<ContactViewModel> { new ContactViewModel { ContactId = 1 } };
            A.CallTo(() => _mapperFake.Map<IQueryable<Contact>, List<ContactViewModel>>(A<IQueryable<Contact>>.Ignored)).Returns(viewModels);

            var result = await _sut.ManageContact() as ViewResult;

            result.Should().NotBeNull();
            result.Model.Should().BeOfType<List<ContactViewModel>>();
        }

        [Fact]
        public async Task DeleteContact_IdIsEmpty_ReturnIdCannotBeNullJson()
        {
            const int emptyId = 0;

            var result = await _sut.DeleteContact(emptyId);

            result.Should().NotBeNull();
            result.Should().BeOfType<JsonResult>();
            result.Value.Should().NotBeNull();
            //result.Value.GetType().GetProperty("Status").GetValue(result.Value).Should().Be("IdCannotBeNull");
            result.GetValueForProperty<string>("Status").Should().Be("IdCannotBeNull");
        }

        [Fact]
        public async Task DeleteContact_ContactByThisIdNotFound_ReturnsContactNotFoundJson()
        {
            A.CallTo(() => _mediatorFake.Send(A<ContactByIdQuery>._, A<CancellationToken>._)).Returns<Contact>(null);

            const int contactId = 15;

            var result = await _sut.DeleteContact(contactId);

            result.GetValueForProperty<string>("Status").Should().Be("ContactNotFound");
        }

        [Fact]
        public async Task DeleteContact_ContactModelExist_SuccessfulyDeleteContact()
        {
            const int contactId = 15;

            A.CallTo(() => _mediatorFake.Send(A<ContactByIdQuery>._, A<CancellationToken>._))
                .Returns(new Contact());

            var result = await _sut.DeleteContact(contactId);

            result.GetValueForProperty<string>("Status").Should().Be("Deleted");
        }

        [Fact]
        public async Task DeleteContact_ContactModelExist_CorrectlyCallsTheDeleteContactCommand()
        {
            const int contactId = 15;

            A.CallTo(() => _mediatorFake.Send(A<ContactByIdQuery>._, A<CancellationToken>._))
                .Returns(new Contact());

             await _sut.DeleteContact(contactId);

            A.CallTo(() => _mediatorFake.Send(A<DeleteContactCommand>._, A<CancellationToken>._))
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
