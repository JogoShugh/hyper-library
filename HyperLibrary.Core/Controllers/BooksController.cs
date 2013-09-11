using System.Net;
using System.Net.Http;
using System.Web.Http;
using HyperLibrary.Core.Commands;
using HyperLibrary.Core.LibraryModel;
using HyperLibrary.Core.Queries;

namespace HyperLibrary.Core.Controllers
{
    public class BooksController : ApiController
    {
        private readonly AllBooksQueryHandler _allBooksQueryHandler;
        private readonly GetBookQueryHandler _bookQueryHandler;
        private readonly AddBookCommandHandler _addBookQueryHandler;
        private readonly DeleteBookCommandHandler _deleteBookCommandHandler;

        public BooksController(AllBooksQueryHandler allBooksQueryHandler, GetBookQueryHandler bookQueryHandler, AddBookCommandHandler addBookQueryHandler, DeleteBookCommandHandler deleteBookCommandHandler)
        {
            _allBooksQueryHandler = allBooksQueryHandler;
            _bookQueryHandler = bookQueryHandler;
            _addBookQueryHandler = addBookQueryHandler;
            _deleteBookCommandHandler = deleteBookCommandHandler;
        }

        // GET api/books
        public HttpResponseMessage Get()
        {
            var booksResource = _allBooksQueryHandler.Query();
            return Request.CreateResponse(HttpStatusCode.OK, booksResource);
        }

        // GET api/books/5
        public HttpResponseMessage Get(int id)
        {
            var bookResource = _bookQueryHandler.Query(id);
            if(bookResource == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.NotFound, "Sorry - we could not find that book");
            }
            return Request.CreateResponse(HttpStatusCode.OK, bookResource);
        }

        // POST api/books
        public HttpResponseMessage Post(Book book)
        {
            var bookResource = _addBookQueryHandler.Execute(book);
            var httpResponseMessage = Request.CreateResponse(HttpStatusCode.Created, bookResource);
            httpResponseMessage.Headers.Location = bookResource.Self.Href;
            return httpResponseMessage;
        }

        // DELETE api/books/5
        public HttpResponseMessage Delete(int id)
        {
            var foundAndDeleted = _deleteBookCommandHandler.Execute(id);
            if (!foundAndDeleted)
            {
                //yes yes, it may not have been found but not deleted. This is a demo, its ok for now
                Request.CreateErrorResponse(HttpStatusCode.NotFound, "Sorry - we could not find that book");
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

    public class FeeController : ApiController
    {
        public HttpResponseMessage Post()
        {
            // pay the fee off
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}