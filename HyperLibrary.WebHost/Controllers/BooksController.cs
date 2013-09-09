using System.Collections.Generic;
using System.Web.Http;
using HyperLibrary.WebHost.Library;

namespace HyperLibrary.WebHost.Controllers
{
    public class BooksController : ApiController
    {
        private readonly IInMemoryBookRepository _bookRepository;

        public BooksController(IInMemoryBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET api/books
        public IEnumerable<Book> Get()
        {
            return _bookRepository.GetAll();
        }

        // GET api/values/5
        public Book Get(int id)
        {
            return _bookRepository.Get(id);
        }
    }
}