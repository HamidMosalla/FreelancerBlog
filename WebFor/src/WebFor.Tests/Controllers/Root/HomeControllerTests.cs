using Microsoft.AspNetCore.Mvc;
using Moq;
using WebFor.Core.Repository;
using WebFor.Web.Controllers;
using Xunit;

namespace WebFor.Tests.Controllers.Root
{
    public class HomeControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public HomeControllerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        void Index_SouldReturn_IndexView()
        {
            var sut = new HomeController(_unitOfWorkMock.Object);

            var result = (ViewResult)sut.Index();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        void About_SouldReturn_AboutView()
        {
            var sut = new HomeController(_unitOfWorkMock.Object);

            var result = (ViewResult)sut.About();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(null, result.ViewName);
        }

        [Fact]
        void Faq_SouldReturn_FaqView()
        {
            var sut = new HomeController(_unitOfWorkMock.Object);

            var result = (ViewResult)sut.Faq();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(null, result.ViewName);
        }

        [Fact]
        void PrivacyPolicy_SouldReturn_PrivacyPolicyView()
        {
            var sut = new HomeController(_unitOfWorkMock.Object);

            var result = (ViewResult)sut.PrivacyPolicy();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(null, result.ViewName);
        }

        [Fact]
        void TermsAndConditions_SouldReturn_TermsAndConditionsView()
        {
            var sut = new HomeController(_unitOfWorkMock.Object);

            var result = (ViewResult)sut.TermsAndConditions();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(null, result.ViewName);
        }

        [Fact]
        void Services_SouldReturn_ServicesView()
        {
            var sut = new HomeController(_unitOfWorkMock.Object);

            var result = (ViewResult)sut.Services();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(null, result.ViewName);
        }

        [Fact]
        void Error_SouldReturn_ErrorView()
        {
            var sut = new HomeController(_unitOfWorkMock.Object);

            var result = (ViewResult)sut.Error(404);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(null, result.ViewName);
        }

        [Fact]
        void Error_SouldAssign_TheStatusCodeViewBagWithErrorCode()
        {
            var sut = new HomeController(_unitOfWorkMock.Object);

            var result = (ViewResult)sut.Error(404);

            Assert.NotNull(result.ViewData["StatusCode"]);
            Assert.Equal(404, result.ViewData["StatusCode"]);
        }
    }
}