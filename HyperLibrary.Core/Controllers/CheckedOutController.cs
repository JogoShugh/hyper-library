using System.Net;
using System.Net.Http;
using System.Web.Http;
using HyperLibrary.Core.Commands;
using HyperLibrary.Core.Queries;

namespace HyperLibrary.Core.Controllers
{
    public class CheckedOutController : ApiController
    {
        private readonly AllCheckedOutBooksQueryHandler _checkedOutBooksQueryHandler;
        private readonly CheckOutCommandHandler _checkoutCommandHandler;
        private readonly GetFinesQueryHandler _finesQuery;

        public CheckedOutController(AllCheckedOutBooksQueryHandler checkedOutBooksQueryHandler, CheckOutCommandHandler checkoutCommandHandler, GetFinesQueryHandler finesQuery)
        {
            _checkedOutBooksQueryHandler = checkedOutBooksQueryHandler;
            _checkoutCommandHandler = checkoutCommandHandler;
            _finesQuery = finesQuery;
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
            var finesResource = _finesQuery.Query();
            if (finesResource != null)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden,finesResource);
            }
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