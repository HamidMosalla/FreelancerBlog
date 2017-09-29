using System;
using System.Threading.Tasks;
using FakeItEasy;
using FreelancerBlog.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FreelancerBlog.UnitTests.HandMadeFakes
{
    public class UserManagerFake : UserManager<ApplicationUser>
    {
        private readonly bool _isUserConfirmed;
        private readonly IdentityResult _identityResult;

        public UserManagerFake(bool isUserConfirmed)
            : base(
                   A.Fake<IUserStore<ApplicationUser>>(),
                   A.Fake<IOptions<IdentityOptions>>(),
                   A.Fake<IPasswordHasher<ApplicationUser>>(),
                   new IUserValidator<ApplicationUser>[0],
                   new IPasswordValidator<ApplicationUser>[0],
                   A.Fake<ILookupNormalizer>(),
                   A.Fake<IdentityErrorDescriber>(),
                   A.Fake<IServiceProvider>(),
                   A.Fake<ILogger<UserManager<ApplicationUser>>>()
                  )
        {
            this._isUserConfirmed = isUserConfirmed;
        }


        public UserManagerFake(bool isUserConfirmed, IdentityResult identityResult)
            : base(A.Fake<IUserStore<ApplicationUser>>(),
                A.Fake<IOptions<IdentityOptions>>(),
                A.Fake<IPasswordHasher<ApplicationUser>>(),
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                A.Fake<ILookupNormalizer>(),
                A.Fake<IdentityErrorDescriber>(),
                A.Fake<IServiceProvider>(),
                A.Fake<ILogger<UserManager<ApplicationUser>>>()
                  )
        {
            this._isUserConfirmed = isUserConfirmed;
            this._identityResult = identityResult;
        }


        public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return Task.FromResult(_identityResult);
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

        public override Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return Task.FromResult("dummy-token");
        }
    }
}
