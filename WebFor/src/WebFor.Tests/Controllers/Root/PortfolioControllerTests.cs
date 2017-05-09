using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using WebFor.Web.Mapper;
using WebFor.Core.Repository;
using WebFor.Web.Controllers;
using Xunit;
using FluentAssertions;
using GenFu;
using WebFor.Core.Domain;
using WebFor.Web.Areas.Admin.ViewModels.Portfolio;

namespace WebFor.UnitTests.Controllers.Root
{
    public class PortfolioControllerTests
    {
        private Mock<IUnitOfWork> _uw;
        private Mock<IWebForMapper> _webForMapper;
        private Mock<IPortfolioRepository> _portfolioRepository;

        public PortfolioControllerTests()
        {
            _uw = new Mock<IUnitOfWork>();
            _webForMapper = new Mock<IWebForMapper>();
            _portfolioRepository = new Mock<IPortfolioRepository>();

            _uw.SetupGet<IPortfolioRepository>(u => u.PortfolioRepository).Returns(_portfolioRepository.Object);

            _webForMapper.Setup(w => w.PortfolioToPorfolioViewModel(It.IsAny<Portfolio>())).Returns(A.New<PortfolioViewModel>());

            _webForMapper.Setup(w => w.PortfolioCollectionToPortfolioViewModelCollection(It.IsAny<List<Portfolio>>()))
                .Returns(A.ListOf<PortfolioViewModel>(10));
        }

        [Fact]
        public async Task Detail_ShoudReturnBadRequest_IfIdIsNotSupplied()
        {
            //Arrange
            var sut = new PortfolioController(_uw.Object, _webForMapper.Object);

            //Act
            var result = (BadRequestResult)await sut.Detail(default(int));

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
            result.StatusCode.Should().Be(400);
        }

        [Fact(Skip ="Temp")]
        public async Task Detail_ShoudReturnNotFound_IfPorfolioDetailNotFound()
        {
            //Arrange
            var sut = new PortfolioController(_uw.Object, _webForMapper.Object);

            //_portfolioRepository.Setup(p => p.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(null);

            //Act
            var result = (NotFoundResult)await sut.Detail(1);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Detail_ShoudReturnRequestedDetailView_IfPorfolioDetailExist()
        {
            //Arrange
            var sut = new PortfolioController(_uw.Object, _webForMapper.Object);

            _portfolioRepository.Setup(p => p.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(A.New<Portfolio>());

            //Act
            var result = (ViewResult)await sut.Detail(1);


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task Detail_ShoudReturnPortfolioDetailViewModel_IfPorfolioDetailExist()
        {
            //Arrange
            var sut = new PortfolioController(_uw.Object, _webForMapper.Object);

            _portfolioRepository.Setup(p => p.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(A.New<Portfolio>());

            //Act
            var result = (ViewResult)await sut.Detail(1);


            //Assert

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<PortfolioViewModel>();
        }

        [Fact]
        public async Task Index_ShoudReturnIndexView_Always()
        {
            //Arrange
            var sut = new PortfolioController(_uw.Object, _webForMapper.Object);

            _portfolioRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(A.ListOf<Portfolio>(10));

            //Act
            var result = (ViewResult)await sut.Index();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task Index_ShoudReturnIndexWithPortfolioViewModel_Always()
        {
            //Arrange
            var sut = new PortfolioController(_uw.Object, _webForMapper.Object);

            _portfolioRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(A.ListOf<Portfolio>(10));

            //Act
            var result = (ViewResult)await sut.Index();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<List<PortfolioViewModel>>();
        }
    }
}
