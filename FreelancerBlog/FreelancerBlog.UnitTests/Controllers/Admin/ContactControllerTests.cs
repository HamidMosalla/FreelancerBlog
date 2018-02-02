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
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Contacts;
using FreelancerBlog.UnitTests.Database;
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
            var viewModels = new List<ContactViewModel> { new ContactViewModel {  ContactId = 1 } };
            A.CallTo(() => _mapperFake.Map<IQueryable<Contact>, List<ContactViewModel>>(A<IQueryable<Contact>>.Ignored)).Returns(viewModels);

            var result = await _sut.ManageContact() as ViewResult;

            result.Should().NotBeNull();
            result.Model.Should().BeOfType<List<ContactViewModel>>();
        }

    }
}
