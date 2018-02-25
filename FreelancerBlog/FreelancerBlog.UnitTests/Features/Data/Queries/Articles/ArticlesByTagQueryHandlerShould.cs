using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Data.Queries.Articles;
using FreelancerBlog.UnitTests.Database;
using Xunit;

namespace FreelancerBlog.UnitTests.Features.Data.Queries.Articles
{
    public class ArticlesByTagQueryHandlerShould : InMemoryContextTest
    {
        private ArticlesByTagQueryHandler _sut;

        protected override void LoadTestData()
        {
            base.LoadTestData();
        }

        [Fact]
        public void Always_ReturnTheCorrectType()
        {
            


        }
    }
}
