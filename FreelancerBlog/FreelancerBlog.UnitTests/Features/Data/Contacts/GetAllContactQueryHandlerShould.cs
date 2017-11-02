using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Contacts;
using FreelancerBlog.Data.Queries.Contacts;
using FreelancerBlog.UnitTests.Database;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;

namespace FreelancerBlog.UnitTests.Features.Data.Contacts
{
    public class GetAllContactQueryHandlerShould : InMemoryContextTest
    {
        private GetAllContactQueryHandler _sut;
        private GetAllContactQuery _message;

        public GetAllContactQueryHandlerShould()
        {
            _message = new GetAllContactQuery();
            _sut = new GetAllContactQueryHandler(Context);
        }

        protected override void LoadTestData()
        {
            var contacts = new List<Contact> { new Contact { ContactId = 12 }, new Contact { ContactId = 2 } };
            Context.Contacts.AddRange(contacts);
            Context.SaveChanges();
        }

        [Fact]
        public void Always_ReturnsObjectOfTypeIQueryableOfContact()
        {
            var result = _sut.Handle(_message);

            result.Should().NotBeNull();
            result.Should().BeOfType<InternalDbSet<Contact>>();
        }

        [Fact]
        public void WhenCalled_ReturnsTheCorrectNumberOfContact()
        {
            var result = _sut.Handle(_message);

            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            result.First().ContactId.Should().Be(12);
        }
    }
}