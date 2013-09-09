using System.Collections.Generic;
using System.Linq;
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

        // GET api/books/5
        public Book Get(int id)
        {
            return _bookRepository.Get(id);
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

    public class CheckedOutController : ApiController
    {
        private readonly IInMemoryBookRepository _bookRepository;

        public CheckedOutController(IInMemoryBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET api/checkedout
        public IEnumerable<Book> Get()
        {
            return _bookRepository.GetAll().Where(book => book.State == BookState.CheckedOut);
        }

        // GET api/checkedout
        public void Post(int bookId)
        {
            var book = _bookRepository.Get(bookId);
            book.CheckOut("fake user");
        }
    }

}