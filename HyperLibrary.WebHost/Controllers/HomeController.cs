using System.Web.Mvc;
using HyperLibrary.Core.LibraryModel;

namespace HyperLibrary.WebHost.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInMemoryBookRepository _bookRepository;

        public HomeController(IInMemoryBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ActionResult Index()
        {
            var books = _bookRepository.GetAll();
            return View(books);
        }

        public ActionResult GetBook(int id)
        {
            var books = _bookRepository.Get(id);
            return View(books);
        }

        public ActionResult CheckOut(int id)
        {
            _bookRepository.Get(id).CheckOut("fake user");
            return RedirectToAction("GetBook", new {id});
        }

        public ActionResult CheckIn(int id)
        {
            _bookRepository.Get(id).CheckIn("fake user");
            return RedirectToAction("GetBook", new { id });
        }
    }
}
