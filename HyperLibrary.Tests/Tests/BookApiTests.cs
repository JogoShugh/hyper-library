using System;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using HyperLibrary.Tests.Server;
using NUnit.Framework;

namespace HyperLibrary.Tests.Tests
{
    public abstract class BookApiTests
    {
        private readonly IApiServer _server;

        private const string BooksRelativeUri = "api/books/1";

        protected BookApiTests(IApiServer apiServer)
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
        public void GetOneBookReturnsSuccessfulStatusCode()
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
        public void CanSuccessfullyGetOneBook()
        {
            var valuesUri = new Uri(_server.BaseAddress, BooksRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage httpResponseMessage = client.GetAsync(valuesUri).Result;
                dynamic result = httpResponseMessage.Content.ReadAsAsync<ExpandoObject>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id,Is.EqualTo(1));
            }
        }

        [Test]
        public void GetNonExistantBookReturnsNotFound()
        {
            var valuesUri = new Uri(_server.BaseAddress, "api/books/203");
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage httpResponseMessage = client.GetAsync(valuesUri).Result;
                Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }
    }
}