using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FreelancerBlog.Areas.Admin.ViewModels.Article;
using FreelancerBlog.Controllers;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Articles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FreelancerBlog.UnitTests.Controllers.Root
{
    public class ArticleControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;

        public ArticleControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task IndexReturnsTheCorrectView()
        {
            var sut = new ArticleController(_mapperMock.Object, _mediatorMock.Object);

            var result = (ViewResult)await sut.Index();

            result.Should().NotBeNull();
            result.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task IndexPassesTheCorrectArticlesToMapMethod()
        {
            var sut = new ArticleController(_mapperMock.Object, _mediatorMock.Object);
            var articles = new[] { new Article { ArticleId = 1 } }.AsQueryable();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAriclesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(articles);

            await sut.Index();

            _mapperMock.Verify(m => m.Map<IQueryable<Article>, List<ArticleViewModel>>(It.Is<IQueryable<Article>>(a => a == articles)));
        }

        [Fact]
        public async Task TagReturnsBadRequestWhenIdIsEmpty()
        {
            var sut = new ArticleController(_mapperMock.Object, _mediatorMock.Object);

            var result = (BadRequestResult)await sut.Tag(0);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task TagReturnsTheCorrectView()
        {
            var sut = new ArticleController(_mapperMock.Object, _mediatorMock.Object);

            var result = (ViewResult)await sut.Tag(1);

            result.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task TagReturnsTheCorrectViewModel()
        {
            var sut = new ArticleController(_mapperMock.Object, _mediatorMock.Object);
            var viewModels = new[] { new ArticleViewModel { ArticleId = 1 } }.ToList();
            _mapperMock.Setup(m => m.Map<IQueryable<Article>, List<ArticleViewModel>>(It.IsAny<IQueryable<Article>>())).Returns(viewModels);

            var result = (ViewResult)await sut.Tag(1);

            result.ViewData.Model.Should().BeOfType<List<ArticleViewModel>>();
            result.ViewData.Model.Should().Be(viewModels);
        }

        [Fact]
        public async Task TagPassesTheCorrectIdToArticlesByTagQuery()
        {
            var sut = new ArticleController(_mapperMock.Object, _mediatorMock.Object);

            var tagId = 1;
            var result = (ViewResult)await sut.Tag(tagId);

            _mediatorMock.Verify(m => m.Send(It.Is<ArticlesByTagQuery>(a => a.TagId == tagId), It.IsAny<CancellationToken>()));
        }

    }
}