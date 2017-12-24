using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Contacts;
using FreelancerBlog.Data.Queries.Contacts;
using FreelancerBlog.UnitTests.Database;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;

namespace FreelancerBlog.UnitTests.Features.Data.Queries.Contacts
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
        public async Task Always_ReturnsObjectOfTypeIQueryableOfContact()
        {
            var result = await _sut.Handle(_message, default(CancellationToken));

            result.Should().NotBeNull();
            result.First().Should().BeOfType<Contact>();
        }

        [Fact]
        public async Task WhenCalled_ReturnsTheCorrectNumberOfContact()
        {
            var result = await _sut.Handle(_message, default(CancellationToken));

            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            result.First().ContactId.Should().Be(12);
        }
    }
}