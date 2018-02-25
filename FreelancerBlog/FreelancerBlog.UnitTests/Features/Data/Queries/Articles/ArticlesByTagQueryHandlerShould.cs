using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Articles;
using FreelancerBlog.Data.Queries.Articles;
using FreelancerBlog.UnitTests.Database;
using Xunit;

namespace FreelancerBlog.UnitTests.Features.Data.Queries.Articles
{
    public class ArticlesByTagQueryHandlerShould : InMemoryContextTest
    {
        private ArticlesByTagQueryHandler _sut;

        public ArticlesByTagQueryHandlerShould()
        {
            _sut = new ArticlesByTagQueryHandler(Context);
        }

        protected override void LoadTestData()
        {
            var articles = new List<Article>
            {
                new Article {ArticleId = 1, ArticleTitle = "A"},
                new Article {ArticleId = 2, ArticleTitle = "B"}
            };

            var articleTags = new List<ArticleTag> { new ArticleTag { ArticleTagId = 1, ArticleTagName = "H" } };

            var articleArticleTag = new List<ArticleArticleTag>
            {
                new ArticleArticleTag {ArticleId = 1, ArticleTagId = 1},
                new ArticleArticleTag {ArticleId = 2, ArticleTagId = 2}
            };

            Context.Articles.AddRange(articles);
            Context.ArticleTags.AddRange(articleTags);
            Context.ArticleArticleTags.AddRange(articleArticleTag);
            Context.SaveChanges();
        }

        [Fact]
        public async Task Always_ReturnTheCorrectType()
        {
            var message = new ArticlesByTagQuery { TagId = 1 };

            var result = await _sut.Handle(message, default(CancellationToken));

            result.First().Should().BeOfType<Article>();
        }
    }
}
