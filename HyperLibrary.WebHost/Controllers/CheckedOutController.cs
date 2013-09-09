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
        [RouteName("DefaultApi")]
        public void Post(int id)
        {
            var book = _bookRepository.Get(id);
            book.CheckOut("fake user");
        }
    }
}