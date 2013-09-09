using System.Net;
using System.Net.Http;
using System.Web.Http;
using HyperLibrary.WebHost.Library;

namespace HyperLibrary.WebHost.Controllers
{
    public class CheckedOutController : ApiController
    {
        private readonly AllCheckedOutBooksQueryHandler _checkedOutBooksQueryHandler;
        private readonly CheckOutCommandHandler _checkoutCommandHandler;

        public CheckedOutController(AllCheckedOutBooksQueryHandler checkedOutBooksQueryHandler, CheckOutCommandHandler checkoutCommandHandler)
        {
            _checkedOutBooksQueryHandler = checkedOutBooksQueryHandler;
            _checkoutCommandHandler = checkoutCommandHandler;
        }

        // GET api/checkedout
        public HttpResponseMessage Get()
        {
            var booksResource = _checkedOutBooksQueryHandler.Query();
            return Request.CreateResponse(HttpStatusCode.OK, booksResource);
        }

        // GET api/checkedout
        public HttpResponseMessage Post(int id)
        {
            var book = _checkoutCommandHandler.Execute(id);
            if (book != null)
            {
                //yes yes, it may not have been found but not deleted. This is a demo, its ok for now
                Request.CreateErrorResponse(HttpStatusCode.NotFound, "Sorry - we could not find that book");
            }
            return Request.CreateResponse(HttpStatusCode.OK, book);
        }
    }
}