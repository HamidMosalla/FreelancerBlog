using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FreelancerBlog.Areas.Admin.ViewModels.Portfolio;
using FreelancerBlog.Controllers;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using FreelancerBlog.Features.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FreelancerBlog.UnitTests.Controllers.Root
{
    public class PortfolioControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;

        public PortfolioControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task DetailReturnBadRequestIfIdIsNotSupplied()
        {
            //Arrange
            var sut = new PortfolioController(_mapperMock.Object, _mediatorMock.Object);

            //Act
            var result = (BadRequestResult)await sut.Detail(default(int));

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task DetailPassTheCorrectIdIntoPortfolioByIdQuery()
        {
            //Arrange
            var sut = new PortfolioController(_mapperMock.Object, _mediatorMock.Object);

            //Act
            var portfolioId = 2;
            await sut.Detail(portfolioId);

            //Assert
            _mediatorMock.Verify(m => m.Send(It.Is<PortfolioByIdQuery>(s => s.PortfolioId == portfolioId), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task DetailReturnNotFoundIfPorfolioDetailNotFound()
        {
            //Arrange
            var sut = new PortfolioController(_mapperMock.Object, _mediatorMock.Object);

            _mediatorMock.Setup(m => m.Send(It.IsAny<PortfolioByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((Portfolio)null);

            //Act
            var result = (NotFoundResult)await sut.Detail(1);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task DetailPassCorrectPortfolioToMapMethod()
        {
            //Arrange
            var sut = new PortfolioController(_mapperMock.Object, _mediatorMock.Object);
            var portfolio = new Portfolio { PortfolioId = 1 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<PortfolioByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(portfolio);

            //Act
            await sut.Detail(1);

            //Assert
            _mapperMock.Verify(m => m.Map<Portfolio, PortfolioViewModel>(It.Is<Portfolio>(p => p == portfolio)));
        }

        [Fact]
        public async Task DetailReturnsCorrectView()
        {
            //Arrange
            var sut = new PortfolioController(_mapperMock.Object, _mediatorMock.Object);
            var portfolio = new Portfolio { PortfolioId = 1 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<PortfolioByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(portfolio);
            _mapperMock.Setup(m => m.Map<Portfolio, PortfolioViewModel>(It.IsAny<Portfolio>())).Returns(It.IsAny<PortfolioViewModel>());

            //Act
            var result = (ViewResult)await sut.Detail(1);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task DetailReturnsCorrectPortfolioViewModel()
        {
            //Arrange
            var sut = new PortfolioController(_mapperMock.Object, _mediatorMock.Object);
            var portfolio = new Portfolio { PortfolioId = 1 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<PortfolioByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(portfolio);
            var portfolioViewModel = new PortfolioViewModel { PortfolioId = 1 };
            _mapperMock.Setup(m => m.Map<Portfolio, PortfolioViewModel>(It.IsAny<Portfolio>())).Returns(portfolioViewModel);

            //Act
            var result = (ViewResult)await sut.Detail(1);

            //Assert

            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<PortfolioViewModel>();
            result.Model.Should().Be(portfolioViewModel);
        }

        [Fact]
        public async Task IndexReturnsCorrectView()
        {
            //Arrange
            var sut = new PortfolioController(_mapperMock.Object, _mediatorMock.Object);

            var portfolios = new[]
            {
                new Portfolio {PortfolioId = 1, PortfolioCategory = "MVC, BS"},
                new Portfolio {PortfolioId = 2, PortfolioCategory = "MVC, BS"}
            }.AsQueryable();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllPortfoliosQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(portfolios);
            _mapperMock.Setup(m => m.Map<List<Portfolio>, List<PortfolioViewModel>>(portfolios.ToList())).Returns(It.IsAny<List<PortfolioViewModel>>);

            //Act
            var result = (ViewResult)await sut.Index();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task IndexPassCorrectPortfoliosToMapMethod()
        {
            //Arrange
            var sut = new PortfolioController(_mapperMock.Object, _mediatorMock.Object);

            var portfolios = new[]
            {
                new Portfolio {PortfolioId = 1, PortfolioCategory = "MVC, BS"},
                new Portfolio {PortfolioId = 2, PortfolioCategory = "MVC, BS"}
            }.AsQueryable();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllPortfoliosQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(portfolios);

            //Act
            await sut.Index();

            //Assert
            _mapperMock.Verify(m => m.Map<IQueryable<Portfolio>, List<PortfolioViewModel>>(It.Is<IQueryable<Portfolio>>(p => p == portfolios)));
        }

        [Fact]
        public async Task IndexPassCorrectPortfoliosAndViewModelToPopulatePortfolioCategoryListCommand()
        {
            //Arrange
            var sut = new PortfolioController(_mapperMock.Object, _mediatorMock.Object);

            var portfolios = new[]
            {
                new Portfolio {PortfolioId = 1, PortfolioCategory = "MVC, BS"},
                new Portfolio {PortfolioId = 2, PortfolioCategory = "MVC, BS"}
            }.AsQueryable();

            var viewModel = new List<PortfolioViewModel>
            {
                new PortfolioViewModel {PortfolioId = 1, PortfolioCategory = "MVC, BS"},
                new PortfolioViewModel {PortfolioId = 2, PortfolioCategory = "MVC, BS"}
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllPortfoliosQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(portfolios);
            _mapperMock.Setup(m => m.Map<IQueryable<Portfolio>, List<PortfolioViewModel>>(It.IsAny<IQueryable<Portfolio>>())).Returns(viewModel);

            //Act
            var result = (ViewResult)await sut.Index();

            //Assert
            _mediatorMock.Verify(m => m.Send(It.Is<PopulatePortfolioCategoryListCommand>(
                                                                p => p.Portfolios == portfolios &&
                                                                p.ViewModel == viewModel),
                                                            It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task IndexReturnsCorrectViewModel()
        {
            //Arrange
            var sut = new PortfolioController(_mapperMock.Object, _mediatorMock.Object);

            var viewModel = new List<PortfolioViewModel>
            {
                new PortfolioViewModel {PortfolioId = 1, PortfolioCategory = "MVC, BS"},
                new PortfolioViewModel {PortfolioId = 2, PortfolioCategory = "MVC, BS"}
            };

            _mapperMock.Setup(m => m.Map<IQueryable<Portfolio>, List<PortfolioViewModel>>(It.IsAny<IQueryable<Portfolio>>())).Returns(viewModel);

            //Act
            var result = (ViewResult)await sut.Index();

            //Assert
            result.Model.Should().Be(viewModel);
        }
    }
}