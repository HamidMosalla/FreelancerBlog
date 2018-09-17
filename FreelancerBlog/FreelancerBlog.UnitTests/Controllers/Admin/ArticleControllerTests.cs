using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using FreelancerBlog.Areas.Admin.Controllers;
using FreelancerBlog.Areas.Admin.ViewModels.Article;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Articles;
using FreelancerBlog.Core.Services.Shared;
using MediatR;
using Mvc.JQuery.DataTables;
using Xunit;

namespace FreelancerBlog.UnitTests.Controllers.Admin
{
    public class ArticleControllerTests
    {
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        private ArticleController _sut;

        public ArticleControllerTests()
        {
            _mapper = A.Fake<IMapper>();
            _mediator = A.Fake<IMediator>();
            _fileManager = A.Fake<IFileManager>();
            _sut = new ArticleController(_fileManager, _mapper, _mediator);
        }

        [Fact]
        public async Task GetArticleTableData_Should_ReturnTheCorrectType()
        {
            var articles = new List<Article> { new Article(), new Article(), new Article() }.AsQueryable();

            A.CallTo(() => _mediator.Send(A<GetAriclesQuery>._, A<CancellationToken>._)).Returns(articles);

            var result = await _sut.GetArticleTableData(new DataTablesParam());

            result.Should().BeOfType<DataTablesResult<ArticleViewModel>>();
        }
    }
}
