using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Core.Commands.Data.Contacts;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Articles;
using FreelancerBlog.Data.Commands.Contacts;
using FreelancerBlog.Data.EntityFramework;
using FreelancerBlog.UnitTests.Database;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace FreelancerBlog.UnitTests.Features.Data.Commands
{
    public class DeleteContactCommandHandlerWrapper : DeleteContactCommandHandler
    {
        public DeleteContactCommandHandlerWrapper(FreelancerBlogContext context) : base(context) { }

        public async Task ExposedHandle(DeleteContactCommand message)
        {
            await base.Handle(message, default(CancellationToken));
        }
    }

    public class DeleteContactCommandShould : InMemoryContextTest
    {
        private readonly DeleteContactCommand _deleteContantMessage;
        private readonly DeleteContactCommandHandlerWrapper _sut;

        public DeleteContactCommandShould()
        {
            _deleteContantMessage = new DeleteContactCommand
            {
                Contact = Context.Contacts.Single(c => c.ContactId == 1)
            };

            _sut = new DeleteContactCommandHandlerWrapper(Context);
        }

        protected override void LoadTestData()
        {
            var contact = new Contact { ContactId = 1, ContactDate = DateTime.Now, ContactName = string.Empty };
            Context.Contacts.Add(contact);
            Context.SaveChanges();
        }

        [Fact]
        public async Task DeleteContactCorrectly()
        {
            await _sut.ExposedHandle(_deleteContantMessage);

            Context.Contacts.SingleOrDefault(c => c.ContactId == _deleteContantMessage.Contact.ContactId).Should().BeNull();
        }
    }
}
