using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Contacts;
using FreelancerBlog.Data.Queries.Contacts;
using FreelancerBlog.UnitTests.Database;
using Xunit;

namespace FreelancerBlog.UnitTests.Features.Data.Contacts
{
    public class ContactByIdQueryHandlerShould : InMemoryContextTest
    {

        private readonly ContactByIdQuery _message;
        private const int ContactId = 1;
        private readonly ContactByIdQueryHandler _sut;

        public ContactByIdQueryHandlerShould()
        {
            _sut = new ContactByIdQueryHandler(Context);
            _message = new ContactByIdQuery { ContactId = ContactId };
        }

        protected override void LoadTestData()
        {
            var contact = new Contact { ContactId = ContactId, ContactName = "Someone Nice" };

            Context.Add(contact);
            Context.SaveChanges();
        }

        [Fact]
        public async Task Always_ReturnsObjectOfTypeContact()
        {
            var result = await _sut.Handle(_message);

            result.Should().BeOfType<Contact>();
        }

        [Fact]
        public async Task WhenCalled_ReturnsTheCorrectContact()
        {
            var result = await _sut.Handle(_message);

            result.Should().NotBeNull();
            result.ContactId.Should().Be(ContactId);
        }
    }
}