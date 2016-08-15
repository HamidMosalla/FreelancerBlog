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

namespace WebFor.Tests.Controllers.Root
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
        }

        [Fact]
        public async Task Detail_ShoudReturnBadRequest_IfIdIsNotSupplied()
        {
            //Arrange
            var sut = new PortfolioController(_uw.Object, _webForMapper.Object);

            //Act
            var result = (BadRequestResult)await sut.Detail(default(int));

            //Assert
            result.Should().BeOfType<BadRequestResult>();
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Detail_ShoudReturnNotFound_IfPorfolioDetailNotFound()
        {
            //Arrange
            var sut = new PortfolioController(_uw.Object, _webForMapper.Object);

            _uw.SetupGet<IPortfolioRepository>(u => u.PortfolioRepository).Returns(_portfolioRepository.Object);

            _portfolioRepository.Setup(p => p.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(null);

            //Act
            var result = (NotFoundResult)await sut.Detail(1);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Detail_ShoudReturnRequestedDetailViewWithModel_IfPorfolioDetailExist()
        {
            //Arrange
            var sut = new PortfolioController(_uw.Object, _webForMapper.Object);

            _uw.SetupGet<IPortfolioRepository>(u => u.PortfolioRepository).Returns(_portfolioRepository.Object);

            _portfolioRepository.Setup(p => p.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(A.New<Portfolio>());

            _webForMapper.Setup(w => w.PortfolioToPorfolioViewModel(It.IsAny<Portfolio>()))
                .Returns(A.New<PortfolioViewModel>());



            //Act
            var result = (ViewResult)await sut.Detail(1);


            //is it ok to have all this here or separate tests?
            //Assert
            result.Should().BeOfType<ViewResult>();
            result.Should().NotBeNull();
            result.ViewName.Should().BeNull();
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<PortfolioViewModel>();
        }
    }
}
