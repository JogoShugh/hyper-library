using System;
using System.Net;
using System.Net.Http;
using HyperLibrary.Tests.Server;
using NUnit.Framework;

namespace HyperLibrary.Tests.Tests
{
    public abstract class RemoveBooksApiTests
    {
        private readonly IApiServer _server;

        private const string BooksRelativeUri = "api/books/1";
        private const string NotFoundBooksRelativeUri = "api/books/100";

        protected RemoveBooksApiTests(IApiServer apiServer)
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
        public void CanSuccessfullyRemoveBook()
        {
            var uri = new Uri(_server.BaseAddress, BooksRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage deleteResponse = client.DeleteAsync(uri.ToString()).Result;
                Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                HttpResponseMessage getResponse = client.GetAsync(uri.ToString()).Result;
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [Test]
        public void WhenBookDoesNotExistNotFoundIsReturned()
        {
            var uri = new Uri(_server.BaseAddress, NotFoundBooksRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage deleteResponse = client.DeleteAsync(uri.ToString()).Result;
                Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }
    }
}