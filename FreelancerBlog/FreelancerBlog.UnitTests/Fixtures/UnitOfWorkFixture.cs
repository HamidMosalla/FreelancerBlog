using FreelancerBlog.Core.Repository;
using Moq;

namespace FreelancerBlog.UnitTests.Fixtures
{
    public class UnitOfWorkFixture
    {

        public Mock<IUnitOfWork> _uw { get; set; }

        public UnitOfWorkFixture()
        {
            _uw = new Mock<IUnitOfWork>();
        }

    }
}
