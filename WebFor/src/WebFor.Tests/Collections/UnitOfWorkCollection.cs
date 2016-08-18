using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Tests.Fixtures;
using Xunit;

namespace WebFor.Tests.Collections
{
    [CollectionDefinition("Unit Of Work Collection")]
    public class UnitOfWorkCollection : ICollectionFixture<UnitOfWorkFixture>
    {
    }
}
