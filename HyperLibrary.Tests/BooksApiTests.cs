using System;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using HyperLibrary.Tests.Server;
using NUnit.Framework;

namespace HyperLibrary.Tests
{
    public abstract class BooksApiTests
    { 
        private readonly IApiServer _server;

        private const string BooksRelativeUri = "api/books";

        protected BooksApiTests(IApiServer apiServer)
        {
            _server = apiServer;
        }

        [SetUp]
        public void Setup()
        {
            _server.Start();
        }

        [TearDown]
        public void TearDown()
        {
            _server.Stop();
        }

        [Test]
        public void GetAllBooksReturnsSuccessfulStatusCode()
        {
            var valuesUri = new Uri(_server.BaseAddress, BooksRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage httpResponseMessage = client.GetAsync(valuesUri).Result;
                Assert.That(httpResponseMessage.IsSuccessStatusCode);
                Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }

        [Test]
        public void CanSuccessfullyGetAllBooks()
        {
            var valuesUri = new Uri(_server.BaseAddress, BooksRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage httpResponseMessage = client.GetAsync(valuesUri).Result;
                dynamic result = httpResponseMessage.Content.ReadAsAsync<ExpandoObject>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(Enumerable.Any(result.Catalog));
            }
        }
    }
}
