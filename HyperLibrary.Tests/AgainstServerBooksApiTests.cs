using System;
using HyperLibrary.Tests.Server;
using HyperLibrary.Tests.Tests;
using NUnit.Framework;

namespace HyperLibrary.Tests
{
    public static class ApiHost
    {
        public static readonly Uri URI = new Uri("http://localhost:42479/");
    }

    [TestFixture]
    public class AgainstServerBooksApiTests : BooksApiTests
    {
        public AgainstServerBooksApiTests(): base(new AspNetApiServer(ApiHost.URI))
        {
        }
    }

    [TestFixture]
    public class AgainstServerBookApiTests : BookApiTests
    {
        public AgainstServerBookApiTests(): base(new AspNetApiServer(ApiHost.URI))
        {
        }
    }

    [TestFixture]
    public class AgainstServerCreateBooksApiTests : CreateBooksApiTests
    {
        public AgainstServerCreateBooksApiTests() : base(new AspNetApiServer(ApiHost.URI))
        {
        }
    }

    [TestFixture]
    public class AgainstServerRemoveBooksApiTests : RemoveBooksApiTests
    {
        public AgainstServerRemoveBooksApiTests()  : base(new AspNetApiServer(ApiHost.URI))
        {
        }
    }
}