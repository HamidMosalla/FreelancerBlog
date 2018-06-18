using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Articles;
using FreelancerBlog.Data.EntityFramework;
using FreelancerBlog.Data.Queries.Articles;
using FreelancerBlog.UnitTests.Database;
using Xunit;

namespace FreelancerBlog.UnitTests.Features.Data.Queries.Articles
{
    public class ArticlesByTagQueryHandlerWrapper : ArticlesByTagQueryHandler
    {
        public ArticlesByTagQueryHandlerWrapper(FreelancerBlogContext context) : base(context)
        {
        }

        public IQueryable<Article> ExposedHandle(ArticlesByTagQuery message)
        {
            return base.Handle(message);
        }
    }

    public class ArticlesByTagQueryHandlerShould : InMemoryContextTest
    {
        private ArticlesByTagQueryHandlerWrapper _sut;

        public ArticlesByTagQueryHandlerShould()
        {
            _sut = new ArticlesByTagQueryHandlerWrapper(Context);
        }

        protected override void LoadTestData()
        {
            var applicationUser = new ApplicationUser { Id = Guid.NewGuid().ToString() };
            var articleComment = new List<ArticleComment> { new ArticleComment { ArticleCommentId = 1 } };
            var articleRating = new List<ArticleRating> { new ArticleRating { ArticleRatingId = 1 } };

            var articles = new List<Article>
            {
                new Article
                {
                    ArticleId = 1,
                    ArticleTitle = "A",
                    ApplicationUser = applicationUser,
                    ArticleComments = articleComment,
                    ArticleRatings = articleRating
                },
                new Article
                {
                    ArticleId = 2,
                    ArticleTitle = "B",
                    ApplicationUser = applicationUser,
                    ArticleComments = articleComment,
                    ArticleRatings = articleRating
                },
                new Article
                {
                    ArticleId = 3,
                    ArticleTitle = "C",
                    ApplicationUser = applicationUser,
                    ArticleComments = articleComment,
                    ArticleRatings = articleRating
                }
            };

            var articleTags = new List<ArticleTag> { new ArticleTag { ArticleTagId = 1, ArticleTagName = "H" } };

            var articleArticleTag = new List<ArticleArticleTag>
            {
                new ArticleArticleTag {ArticleId = 1, ArticleTagId = 1},
                new ArticleArticleTag {ArticleId = 2, ArticleTagId = 2},
                new ArticleArticleTag{ ArticleId = 3, ArticleTagId = 1}
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

            var result = _sut.ExposedHandle(message);

            result.First().Should().BeOfType<Article>();
        }

        [Fact]
        public async Task Always_ReturnTheCorrectArticles()
        {
            var message = new ArticlesByTagQuery { TagId = 1 };

            var result = _sut.ExposedHandle(message);

            result.Count().Should().Be(2);
            result.Any(r => r.ArticleId == 1).Should().BeTrue();
            result.Any(r => r.ArticleId == 3).Should().BeTrue();
        }
    }
}
