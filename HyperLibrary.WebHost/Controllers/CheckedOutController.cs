using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HyperLibrary.WebHost.Library;

namespace HyperLibrary.WebHost.Controllers
{
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