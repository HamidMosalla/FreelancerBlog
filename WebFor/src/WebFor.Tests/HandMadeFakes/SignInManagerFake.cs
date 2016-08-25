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

        public SignInManagerFake(IHttpContextAccessor contextAccessor, SignInResult signInResult)
        : base(new UserManagerFake(isUserConfirmed:false),
              contextAccessor,
              new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<ILogger<SignInManager<ApplicationUser>>>().Object)
        {
            _signInResult = signInResult;
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
    }
}
