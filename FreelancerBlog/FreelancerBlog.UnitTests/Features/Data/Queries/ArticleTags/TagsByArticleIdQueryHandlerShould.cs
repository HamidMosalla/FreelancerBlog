using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.ArticleTags;
using FreelancerBlog.Data.Queries.ArticleTags;
using FreelancerBlog.UnitTests.Database;
using Xunit;

namespace FreelancerBlog.UnitTests.Features.Data.Queries.ArticleTags
{
    public class TagsByArticleIdQueryHandlerShould: InMemoryContextTest
    {
        protected override void LoadTestData()
        {
            var articleTags = new List<ArticleTag>
            {
                new ArticleTag {ArticleTagId = 1, ArticleTagName = "TagNameOne"},
                new ArticleTag {ArticleTagId = 2, ArticleTagName = "TagNameTwo"}
            };

           var articleToArticleTags = new List<ArticleArticleTag>
           {
               new ArticleArticleTag { ArticleId = 1, ArticleTagId = 1 },
               new ArticleArticleTag {  ArticleId = 1, ArticleTagId = 2}
           };

            Context.ArticleArticleTags.AddRange(articleToArticleTags);
            Context.ArticleTags.AddRange(articleTags);
            Context.SaveChanges();
        }

        [Fact]
        public async Task GivenArticleTagId_ReturnsTheCorrectType()
        {
            var tagsByArticleIdQuery = new TagsByArticleIdQuery {ArticleId = 1};
            var sut = new TagsByArticleIdQueryHandler(Context);

            var result = await sut.Handle(tagsByArticleIdQuery, default(CancellationToken));

            result.Should().NotBeNull();
            result.Should().BeOfType<string>();
        }

        [Fact]
        public async Task GivenArticleTagId_ReturnsTheCorrectTagString()
        {
            var tagsByArticleIdQuery = new TagsByArticleIdQuery { ArticleId = 1 };
            var sut = new TagsByArticleIdQueryHandler(Context);

            var result = await sut.Handle(tagsByArticleIdQuery, default(CancellationToken));

            result.Should().Be("TagNameOne,TagNameTwo");
        }
    }
}
