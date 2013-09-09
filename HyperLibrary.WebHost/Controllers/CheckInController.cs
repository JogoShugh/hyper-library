using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HyperLibrary.WebHost.Library;

namespace HyperLibrary.WebHost.Controllers
{
    public class CheckInController : ApiController
    {
        private readonly IInMemoryBookRepository _bookRepository;

        public CheckInController(IInMemoryBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET api/checkedin
        public IEnumerable<Book> Get()
        {
            return _bookRepository.GetAll().Where(book => book.State == BookState.CheckedIn);
        }

        // GET api/checkedin
        public void Post(int bookId)
        {
            var book = _bookRepository.Get(bookId);
            book.CheckIn("fake user");
        }
    }
}