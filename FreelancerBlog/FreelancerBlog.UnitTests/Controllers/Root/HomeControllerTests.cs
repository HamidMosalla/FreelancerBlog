//using FreelancerBlog.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using Xunit;
//using Xunit.Abstractions;

//namespace FreelancerBlog.UnitTests.Controllers.Root
//{
//    public class HomeControllerTests 
//    {
//        [Fact]
//        [Trait("Category", "DefaultView")]
//        void Index_SouldReturn_IndexView()
//        {
//            var sut = new HomeController();

//            var result = (ViewResult)sut.Index();

//            Assert.NotNull(result);
//            Assert.IsType<ViewResult>(result);
//            Assert.Equal("Index", result.ViewName);
//        }

//        [Fact]
//        [Trait("Category", "DefaultView")]
//        void About_SouldReturn_AboutView()
//        {
//            var sut = new HomeController();

//            var result = (ViewResult)sut.About();

//            Assert.NotNull(result);
//            Assert.IsType<ViewResult>(result);
//            Assert.Null(result.ViewName);
//        }

//        [Fact]
//        [Trait("Category", "DefaultView")]
//        void Faq_SouldReturn_FaqView()
//        {
//            var sut = new HomeController();

//            var result = (ViewResult)sut.Faq();

//            Assert.NotNull(result);
//            Assert.IsType<ViewResult>(result);
//            Assert.Null(result.ViewName);
//        }

//        [Fact]
//        [Trait("Category", "DefaultView")]
//        void PrivacyPolicy_SouldReturn_PrivacyPolicyView()
//        {
//            var sut = new HomeController();

//            var result = (ViewResult)sut.PrivacyPolicy();

//            Assert.NotNull(result);
//            Assert.IsType<ViewResult>(result);
//            Assert.Null(result.ViewName);
//        }

//        [Fact]
//        [Trait("Category", "DefaultView")]
//        void TermsAndConditions_SouldReturn_TermsAndConditionsView()
//        {
//            var sut = new HomeController();

//            var result = (ViewResult)sut.TermsAndConditions();

//            Assert.NotNull(result);
//            Assert.IsType<ViewResult>(result);
//            Assert.Null(result.ViewName);
//        }

//        [Fact]
//        [Trait("Category", "DefaultView")]
//        void Services_SouldReturn_ServicesView()
//        {
//            var sut = new HomeController();

//            var result = (ViewResult)sut.Services();

//            Assert.NotNull(result);
//            Assert.IsType<ViewResult>(result);
//            Assert.Null(result.ViewName);
//        }

//        [Fact]
//        [Trait("Category", "DefaultView")]
//        void Error_SouldReturn_ErrorView()
//        {
//            var sut = new HomeController();

//            var result = (ViewResult)sut.Error(404);

//            Assert.NotNull(result);
//            Assert.IsType<ViewResult>(result);
//            Assert.Null(result.ViewName);
//        }

//        [Fact]
//        void Error_SouldAssign_TheStatusCodeViewBagWithErrorCode()
//        {
//            var sut = new HomeController();

//            var result = (ViewResult)sut.Error(404);

//            Assert.NotNull(result.ViewData["StatusCode"]);
//            Assert.Equal(404, result.ViewData["StatusCode"]);
//        }
//    }
//}