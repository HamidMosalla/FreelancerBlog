using System.Security.Claims;
using System.Threading.Tasks;
using FakeItEasy;
using FreelancerBlog.Core.DomainModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FreelancerBlog.UnitTests.HandMadeFakes
{
    public class SignInManagerFake : SignInManager<ApplicationUser>
    {
        private SignInResult _signInResult;
        private ExternalLoginInfo _externalLoginInfo;

        public SignInManagerFake(IHttpContextAccessor contextAccessor, SignInResult signInResult)
        : base(new UserManagerFake(isUserConfirmed: false),
              contextAccessor,
              A.Fake< IUserClaimsPrincipalFactory<ApplicationUser>>(),
              A.Fake<IOptions<IdentityOptions>>(),
              A.Fake<ILogger<SignInManager<ApplicationUser>>>(),
              A.Fake<IAuthenticationSchemeProvider>(),
              A.Fake<IUserConfirmation<ApplicationUser>>()
            )
        {
            _signInResult = signInResult;
        }

        public SignInManagerFake(IHttpContextAccessor contextAccessor, SignInResult signInResult, ExternalLoginInfo externalLoginInfo, bool isSignIn = false)
        : base(new UserManagerFake(isUserConfirmed: false),
              contextAccessor,
            A.Fake<IUserClaimsPrincipalFactory<ApplicationUser>>(),
            A.Fake<IOptions<IdentityOptions>>(),
            A.Fake<ILogger<SignInManager<ApplicationUser>>>(),
            A.Fake<IAuthenticationSchemeProvider>(),
              A.Fake<IUserConfirmation<ApplicationUser>>()
              )
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

        public override bool IsSignedIn(ClaimsPrincipal principal)
        {
            return true;
        }
    }
}
