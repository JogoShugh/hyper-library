using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HyperLibrary.WebHost.Library;

namespace HyperLibrary.WebHost.Controllers
{
    public class BooksController : ApiController
    {
        private readonly IInMemoryBookRepository _bookRepository;
        private readonly GetBookQueryHandler _bookQueryHandler;

        public BooksController(IInMemoryBookRepository bookRepository, GetBookQueryHandler bookQueryHandler)
        {
            _bookRepository = bookRepository;
            _bookQueryHandler = bookQueryHandler;
        }

        // GET api/books
        public IEnumerable<Book> Get()
        {
            return _bookRepository.GetAll();
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
        public void Post(Book book)
        {
            _bookRepository.Add(book);
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