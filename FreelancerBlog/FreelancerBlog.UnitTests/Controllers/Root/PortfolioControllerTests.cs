using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FreelancerBlog.Areas.Admin.ViewModels.Portfolio;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Controllers;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using GenFu;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FreelancerBlog.UnitTests.Controllers.Root
{
    public class PortfolioControllerTests
    {
        private Mock<IMediator> _mediator;
        private Mock<IMapper> _mapper;

        public PortfolioControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Detail_ShoudReturnBadRequest_IfIdIsNotSupplied()
        {
            //Arrange
            var sut = new PortfolioController(_mapper.Object, _mediator.Object);

            //Act
            var result = (BadRequestResult)await sut.Detail(default(int));

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Detail_ShoudReturnNotFound_IfPorfolioDetailNotFound()
        {
            //Arrange
            var sut = new PortfolioController(_mapper.Object, _mediator.Object);

            _mediator.Setup(m => m.Send(It.IsAny<PortfolioByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((Portfolio)null);

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
            var sut = new PortfolioController(_mapper.Object, _mediator.Object);

            //_portfolioRepository.Setup(p => p.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(A.New<Portfolio>());

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
            var sut = new PortfolioController(_mapper.Object, _mediator.Object);

            // _portfolioRepository.Setup(p => p.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(A.New<Portfolio>());

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
            var sut = new PortfolioController(_mapper.Object, _mediator.Object);

            //_portfolioRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(A.ListOf<Portfolio>(10));

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
            var sut = new PortfolioController(_mapper.Object, _mediator.Object);

            //_portfolioRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(A.ListOf<Portfolio>(10));

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