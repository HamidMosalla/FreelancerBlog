using System.Collections.Generic;
using System.Security.Claims;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.UnitTests.Extensions
{
    public static class ControllerTestHelpers
    {
        private static void SetFakeHttpContextIfNotAlreadySet(Controller controller)
        {
            if (controller.ControllerContext.HttpContext == null) controller.ControllerContext.HttpContext = FakeHttpContext();
        }

        private static HttpContext FakeHttpContext()
        {
            var fakeHttpContext = A.Fake<HttpContext>();
            var fakeHttpRequest = A.Fake<HttpRequest>();
            var fakeHttpResponse = A.Fake<HttpResponse>();

            A.CallTo(() => fakeHttpContext.Request).Returns(fakeHttpRequest);
            A.CallTo(() => fakeHttpContext.Response).Returns(fakeHttpResponse);
            A.CallTo(() => fakeHttpRequest.Cookies[A<string>._]).Returns(A<string>._);

            return fakeHttpContext;
        }

        public static void SetDefaultHttpContext(this Controller controller)
        {
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        public static void SetFakeHttpRequestSchemeTo(this Controller controller, string requestScheme)
        {
            SetFakeHttpContextIfNotAlreadySet(controller);
            A.CallTo(() => controller.Request.Scheme).Returns(requestScheme);
        }

        public static void SetFakeUser(this Controller controller, string userId)
        {
            SetFakeHttpContextIfNotAlreadySet(controller);

            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) }));

            A.CallTo(() => controller.HttpContext.User).Returns(claimsPrincipal);
        }

        public static void SetFakeUserName(this Controller controller, string userName)
        {
            SetFakeHttpContextIfNotAlreadySet(controller);

            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Name, userName) }));
            A.CallTo(() => controller.HttpContext.User).Returns(claimsPrincipal);
        }

        public static void SetFakeUserWithCookieAuthenticationType(this Controller controller, string userId)
        {
            SetFakeHttpContextIfNotAlreadySet(controller);

            var identity = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userId)
                }, IdentityConstants.ApplicationScheme);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            A.CallTo(() => controller.HttpContext.User).Returns(claimsPrincipal);
        }

        public static void AddModelStateErrorWithErrorMessage(this Controller controller, string errorMessage)
        {
            controller.ViewData.ModelState.AddModelError("Error", errorMessage);
        }

        public static void AddModelStateError(this Controller controller)
        {
            controller.ViewData.ModelState.AddModelError("Error", "test");
        }

        public static void SetClaims(this Controller controller, List<Claim> claims)
        {
            SetFakeHttpContextIfNotAlreadySet(controller);

            var claimsIdentity = new ClaimsIdentity();
            claims.ForEach(claim => claimsIdentity.AddClaim(claim));
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            A.CallTo(() => controller.HttpContext.User).Returns(claimsPrincipal);
        }

        public static HttpContext GetMockHttpContext(this Controller controller)
        {
            SetFakeHttpContextIfNotAlreadySet(controller);
            return controller.HttpContext;
        }

        public static HttpRequest GetMockHttpRequest(this Controller controller)
        {
            SetFakeHttpContextIfNotAlreadySet(controller);
            return controller.Request;
        }

        public static HttpResponse GetMockHttpResponse(this Controller controller)
        {
            SetFakeHttpContextIfNotAlreadySet(controller);
            return controller.Response;
        }

        public static IUrlHelper SetFakeIUrlHelper(this Controller controller)
        {
            controller.Url = A.Fake<IUrlHelper>();
            return controller.Url;
        }
    }
}