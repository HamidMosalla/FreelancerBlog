using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using FreelancerBlog.Data.Queries.ArticleTags;
using FreelancerBlog.UnitTests.Database;
using Xunit;

namespace FreelancerBlog.UnitTests.Features.Data.Queries.ArticleTags
{
    public class GetCurrentArticleTagsQueryHandlerShould : InMemoryContextTest
    {
        private readonly GetCurrentArticleTagsQuery _getCurrentArticleTagsQuery;


        public GetCurrentArticleTagsQueryHandlerShould()
        {
            this._getCurrentArticleTagsQuery = new GetCurrentArticleTagsQuery { ArticleId = 1 };
        }

        protected override void LoadTestData()
        {
            var articleArticleTags = new List<ArticleArticleTag>
            {
                new ArticleArticleTag {ArticleId = 1, ArticleTagId = 1},
                new ArticleArticleTag {ArticleId = 1, ArticleTagId = 2}
            };

            var articleTags = new List<ArticleTag>
            {
                new ArticleTag {ArticleTagId = 1, ArticleTagName = "Tag 1 From Article 1"},
                new ArticleTag {ArticleTagId = 2, ArticleTagName = "Tag 2 From Article 2"}
            };

            Context.ArticleTags.AddRange(articleTags);
            Context.ArticleArticleTags.AddRange(articleArticleTags);
            Context.SaveChanges();
        }

        [Fact]
        public async Task Always_ReturnTheCorrectType()
        {
            var sut = new GetCurrentArticleTagsQueryHandler(Context);

            var result = await sut.Handle(_getCurrentArticleTagsQuery, default(CancellationToken));

            result.Should().BeOfType<List<ArticleTag>>();
        }

        [Fact]
        public async Task GivenAnGetCurrentArticleTagsQuery_ReturnTheCorrectArticleTags()
        {
            var sut = new GetCurrentArticleTagsQueryHandler(Context);

            var result = await sut.Handle(_getCurrentArticleTagsQuery, default(CancellationToken));

            result.Count.Should().Be(2);
            result[0].ArticleTagName.Contains("1 From Article").Should().Be(true);
            result[1].ArticleTagName.Contains("2 From Article").Should().Be(true);
        }
    }
}
