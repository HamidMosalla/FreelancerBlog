using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using FreelancerBlog.Data.Queries.Portfolios;
using FreelancerBlog.UnitTests.Database;
using Xunit;

namespace FreelancerBlog.UnitTests.Features.Data.Queries.Portfolios
{
    public class PortfolioByIdQueryHandlerShould : InMemoryContextTest
    {
        protected override void LoadTestData()
        {
            var portfolios = new List<Portfolio> { new Portfolio { PortfolioId = 1 }, new Portfolio { PortfolioId = 2 } };
            Context.Portfolios.AddRange(portfolios);
            Context.SaveChanges();
        }

        [Fact]
        public async Task Always_ReturnsTheCorrectType()
        {
            var sut = new PortfolioByIdQueryHandler(Context);
            var message = new PortfolioByIdQuery { PortfolioId = 1 };

            var result = await sut.Handle(message);

            result.Should().BeOfType<Portfolio>();
        }

        [Fact]
        public async Task Always_ReturnsTheCorrectPortfolio()
        {
            var sut = new PortfolioByIdQueryHandler(Context);
            var message = new PortfolioByIdQuery { PortfolioId = 1 };

            var result = await sut.Handle(message);

            result.PortfolioId.Should().Be(message.PortfolioId);
        }

    }
}
