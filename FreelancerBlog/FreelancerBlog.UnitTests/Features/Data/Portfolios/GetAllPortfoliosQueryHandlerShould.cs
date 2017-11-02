using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using FreelancerBlog.Data.Queries.Portfolios;
using FreelancerBlog.UnitTests.Database;
using Xunit;

namespace FreelancerBlog.UnitTests.Features.Data.Portfolios
{
    public class GetAllPortfoliosQueryHandlerShould : InMemoryContextTest
    {
        private GetAllPortfoliosQuery _message;
        private const int PortfolioId = 1;
        private GetAllPortfoliosQueryHandler _sut;

        public GetAllPortfoliosQueryHandlerShould()
        {
            _message = new GetAllPortfoliosQuery();
            _sut = new GetAllPortfoliosQueryHandler(Context);
        }

        protected override void LoadTestData()
        {
            var portfolio = new Portfolio { PortfolioId = PortfolioId, PortfolioTitle = "First Portfolio" };

            Context.Add(portfolio);
            Context.SaveChanges();
        }

        [Fact]
        public void WhenCalled_ReturnsObjectOfTypeIQueryablePortfolio()
        {
            var result = _sut.Handle(_message).ToList();

            result.Should().NotBeNull();
            result.Should().BeOfType<List<Portfolio>>();
        }

        [Fact]
        public void WhenCalled_ReturnsTheCorrectNumberOfPortfolio()
        {
            var result = _sut.Handle(_message).ToList();

            result.Count.Should().Be(1);
        }
    }
}
