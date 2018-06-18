using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using FreelancerBlog.Data.EntityFramework;
using FreelancerBlog.Data.Queries.Portfolios;
using FreelancerBlog.UnitTests.Database;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;

namespace FreelancerBlog.UnitTests.Features.Data.Queries.Portfolios
{
    public class GetAllPortfoliosQueryHandlerWrapper : GetAllPortfoliosQueryHandler
    {
        public GetAllPortfoliosQueryHandlerWrapper(FreelancerBlogContext context) : base(context)
        {
        }

        public IQueryable<Portfolio> ExposedHandle(GetAllPortfoliosQuery message)
        {
            return base.Handle(message);
        }
    }

    public class GetAllPortfoliosQueryHandlerShould : InMemoryContextTest
    {
        private readonly GetAllPortfoliosQueryHandlerWrapper _sut;

        public GetAllPortfoliosQueryHandlerShould()
        {
            _sut = new GetAllPortfoliosQueryHandlerWrapper(Context);
        }

        protected override void LoadTestData()
        {
            var portfolios = new List<Portfolio>
            {
                new Portfolio {PortfolioId = 1, PortfolioTitle = "First Portfolio"},
                new Portfolio {PortfolioId = 2, PortfolioTitle = "Second Portfolio"}
            };

            Context.Portfolios.AddRange(portfolios);
            Context.SaveChanges();
        }

        [Fact]
        public async Task WhenCalled_ReturnsObjectOfTypeIQueryablePortfolio()
        {
            var result = _sut.ExposedHandle(new GetAllPortfoliosQuery());

            result.Should().NotBeNull();
            //TODO: find a better way to pass this
            result.Should().BeOfType<InternalDbSet<Portfolio>>();
        }

        [Fact]
        public async Task WhenCalled_ReturnsTheCorrectNumberOfPortfolio()
        {
            var result = _sut.ExposedHandle(new GetAllPortfoliosQuery());

            result.ToList().Count.Should().Be(2);
        }
    }
}
