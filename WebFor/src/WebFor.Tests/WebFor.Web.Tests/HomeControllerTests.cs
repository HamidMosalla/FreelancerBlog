using Microsoft.AspNetCore.Mvc;
using Moq;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.Repository;
using WebFor.Web.Controllers;
using Xunit;

namespace WebFor.Tests.WebFor.Web.Tests
{
    public class HomeControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public HomeControllerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        void Index_SouldReturn_NotNull()
        {
            var sut = new HomeController(_unitOfWorkMock.Object);

            var result = (ViewResult)sut.Index();

            Assert.NotNull(result);
        }

        [Fact]
        void Index_SouldReturn_IndexView()
        {
            var sut = new HomeController(_unitOfWorkMock.Object);

            var result = (ViewResult)sut.Index();

            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        void Index_SouldReturn_TheSameType()
        {
            var sut = new HomeController(_unitOfWorkMock.Object);

            var result = (ViewResult)sut.Index();

            Assert.IsType<ViewResult>(result);
        }
    }
}