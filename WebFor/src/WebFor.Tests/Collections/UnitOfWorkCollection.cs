using WebFor.UnitTests.Fixtures;
using Xunit;

namespace WebFor.UnitTests.Collections
{
    [CollectionDefinition("Unit Of Work Collection")]
    public class UnitOfWorkCollection : ICollectionFixture<UnitOfWorkFixture>
    {
    }
}
