using System;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using HyperLibrary.Tests.Server;
using NUnit.Framework;

namespace HyperLibrary.Tests
{
    public abstract class CreateBooksApiTests
    {
        private readonly IApiServer _server;

        private const string BooksRelativeUri = "api/books";

        protected CreateBooksApiTests(IApiServer apiServer)
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
        public void CanSuccessfullyAddBook()
        {
            var valuesUri = new Uri(_server.BaseAddress, BooksRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                var newBook = new
                {
                    Title = "My very first book",
                    Description = "It's super cool",
                    Author = "Ammeep"
                };
                HttpResponseMessage httpResponseMessage = client.PostAsJsonAsync(valuesUri.ToString(), newBook).Result;
                dynamic result = httpResponseMessage.Content.ReadAsAsync<ExpandoObject>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Title, Is.EqualTo(newBook.Title));
                Assert.That(result.Author, Is.EqualTo(newBook.Author));
                Assert.That(result.Description, Is.EqualTo(newBook.Description));
                Assert.That(result.Id, Is.Not.Null);
            }
        }

        [Test]
        public void SuccessfullyAddedBookReturnsCreatedHttpStatusCode()
        {
            var valuesUri = new Uri(_server.BaseAddress, BooksRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                var newBook = new
                {
                    Title = "My very first book",
                    Description = "It's super cool",
                    Author = "Ammeep"
                };
                HttpResponseMessage httpResponseMessage = client.PostAsJsonAsync(valuesUri.ToString(), newBook).Result;
                Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            }
        }

        [Test]
        public void SuccessfullyAddedBookReturnsLocationHeader()
        {
            var valuesUri = new Uri(_server.BaseAddress, BooksRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                var newBook = new
                {
                    Title = "My very first book",
                    Description = "It's super cool",
                    Author = "Ammeep"
                };
                HttpResponseMessage httpResponseMessage = client.PostAsJsonAsync(valuesUri.ToString(), newBook).Result;
                dynamic result = httpResponseMessage.Content.ReadAsAsync<ExpandoObject>().Result;
                int id = (int)result.Id;
                Assert.That(httpResponseMessage.Headers.Location.AbsolutePath, Is.EqualTo(string.Format("/api/Books/{0}", id)));
            }
        }
    }
}