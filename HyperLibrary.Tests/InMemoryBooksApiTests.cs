using HyperLibrary.Tests.Server;
using NUnit.Framework;

namespace HyperLibrary.Tests
{
    [TestFixture]
    public class InMemoryBooksApiTests : BooksApiTests
    {
        public InMemoryBooksApiTests() : base(new InMemoryApiServer())
        {
        }
    }

    [TestFixture]
    public class InMemoryBookApiTests : BookApiTests
    {
        public InMemoryBookApiTests(): base(new InMemoryApiServer())
        {
        }
    }

    [TestFixture]
    public class InMemoryCreateBooksApiTests : CreateBooksApiTests
    {
        public InMemoryCreateBooksApiTests(): base(new InMemoryApiServer())
        {
        }
    }

    [TestFixture]
    public class InMemoryRemoveBooksApiTests : RemoveBooksApiTests
    {
        public InMemoryRemoveBooksApiTests() : base(new InMemoryApiServer())
        {
        }
    }
}