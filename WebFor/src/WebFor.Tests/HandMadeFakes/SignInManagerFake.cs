using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WebFor.Tests.Fixtures;
using WebFor.Core.Domain;

namespace WebFor.Tests.HandMadeFakes
{
    public class SignInManagerFake : SignInManager<ApplicationUser>
    {
        private SignInResult _signInResult;
        private ExternalLoginInfo _externalLoginInfo;

        public SignInManagerFake(IHttpContextAccessor contextAccessor, SignInResult signInResult)
        : base(new UserManagerFake(isUserConfirmed: false),
              contextAccessor,
              new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<ILogger<SignInManager<ApplicationUser>>>().Object)
        {
            _signInResult = signInResult;
        }

        public SignInManagerFake(IHttpContextAccessor contextAccessor, SignInResult signInResult, ExternalLoginInfo externalLoginInfo)
        : base(new UserManagerFake(isUserConfirmed: false),
              contextAccessor,
              new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<ILogger<SignInManager<ApplicationUser>>>().Object)
        {
            _signInResult = signInResult;
            _externalLoginInfo = externalLoginInfo;
        }

        public override Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null)
        {
            return Task.FromResult(0);
        }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return Task.FromResult(_signInResult);
        }

        public override Task SignOutAsync()
        {
            return Task.FromResult(0);
        }

        public override Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null)
        {
            return Task.FromResult<ExternalLoginInfo>(_externalLoginInfo);
        }

        public override Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent)
        {
            return Task.FromResult(_signInResult);
        }
    }
}
