using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WebFor.Core.Domain;

namespace WebFor.Tests.HandMadeFakes
{
    public class UserManagerFake : UserManager<ApplicationUser>
    {
        private readonly bool _isUserConfirmed;

        public UserManagerFake(bool isUserConfirmed)
            : base(new Mock<IUserStore<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<ApplicationUser>>().Object,
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        {
            this._isUserConfirmed = isUserConfirmed;
        }

        public override Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return Task.FromResult(new ApplicationUser { Email = email, EmailConfirmed = _isUserConfirmed });
        }

        public override Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            return Task.FromResult(user.Email == "test@test.com");
        }

        public override Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return Task.FromResult("---------------");
        }
    }
}
