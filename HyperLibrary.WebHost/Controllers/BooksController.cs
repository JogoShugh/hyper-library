using System.Net;
using System.Net.Http;
using System.Web.Http;
using HyperLibrary.WebHost.Library;

namespace HyperLibrary.WebHost.Controllers
{
    public class BooksController : ApiController
    {
        private readonly IInMemoryBookRepository _bookRepository;
        private readonly AllBooksQueryHandler _allBooksQueryHandler;
        private readonly GetBookQueryHandler _bookQueryHandler;
        private readonly AddBookCommandHandler _addBookQueryHandler;

        public BooksController(IInMemoryBookRepository bookRepository, AllBooksQueryHandler allBooksQueryHandler, GetBookQueryHandler bookQueryHandler, AddBookCommandHandler addBookQueryHandler)
        {
            _bookRepository = bookRepository;
            _allBooksQueryHandler = allBooksQueryHandler;
            _bookQueryHandler = bookQueryHandler;
            _addBookQueryHandler = addBookQueryHandler;
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
            httpResponseMessage.Headers.Location = bookResource.Self.Uri;
            return httpResponseMessage;
        }

        // PUT api/book/5
        public void Put(int id, [FromBody]Book book)
        {
            _bookRepository.Replace(book);
        }

        // DELETE api/books/5
        public void Delete(int id)
        {
            _bookRepository.Delete(id);
        }
    }
}