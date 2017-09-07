using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
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
        private readonly IMediator _mediatorFake;
        private readonly IMapper _mapperFake;

        public ArticleControllerTests()
        {
            _mediatorFake = A.Fake<IMediator>();
            _mapperFake = A.Fake<IMapper>();
        }

        [Fact]
        public async Task Index_Always_ReturnsTheCorrectView()
        {
            var sut = new ArticleController(_mapperFake, _mediatorFake);

            var result = (ViewResult)await sut.Index();

            result.Should().NotBeNull();
            result.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task Index_Always_PassesTheCorrectArticlesToMapMethod()
        {
            var sut = new ArticleController(_mapperFake, _mediatorFake);
            var articles = new[] { new Article { ArticleId = 1 } }.AsQueryable();

            A.CallTo(() => _mediatorFake.Send(A<GetAriclesQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(articles);

            await sut.Index();

            A.CallTo(() => _mapperFake.Map<IQueryable<Article>, List<ArticleViewModel>>(articles)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public async Task Tag_IdIsEmpty_ReturnsBadRequest()
        {
            var sut = new ArticleController(_mapperFake, _mediatorFake);

            var result = (BadRequestResult)await sut.Tag(0);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Tag_IdNotEmpty_ReturnsTheCorrectView()
        {
            var sut = new ArticleController(_mapperFake, _mediatorFake);

            var result = (ViewResult)await sut.Tag(1);

            result.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task Tag_IdNotEmpty_ReturnsTheCorrectViewModel()
        {
            var sut = new ArticleController(_mapperFake, _mediatorFake);
            var viewModels = new[] { new ArticleViewModel { ArticleId = 1 } }.ToList();
            A.CallTo(() => _mapperFake.Map<IQueryable<Article>, List<ArticleViewModel>>(A<IQueryable<Article>>.Ignored)).Returns(viewModels);

            var result = (ViewResult)await sut.Tag(1);

            result.ViewData.Model.Should().BeOfType<List<ArticleViewModel>>();
            result.ViewData.Model.Should().Be(viewModels);
        }

        [Fact]
        public async Task Tag_IdNotEmpty_PassesTheCorrectIdToArticlesByTagQuery()
        {
            var sut = new ArticleController(_mapperFake, _mediatorFake);
            var tagId = 1;
            var result = (ViewResult)await sut.Tag(tagId);

            A.CallTo(() => _mediatorFake.Send(A<ArticlesByTagQuery>.That.Matches(a => a.TagId == tagId), A<CancellationToken>._)).MustHaveHappened();
        }
    }
}