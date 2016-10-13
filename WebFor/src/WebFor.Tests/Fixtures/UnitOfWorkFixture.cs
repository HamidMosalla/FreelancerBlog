using Moq;
using WebFor.Core.Repository;

namespace WebFor.UnitTests.Fixtures
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
