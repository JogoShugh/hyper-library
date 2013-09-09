using System.Net;
using System.Net.Http;
using System.Web.Http;
using HyperLibrary.Core.Library;

namespace HyperLibrary.Core.Controllers
{
    public class CheckInController : ApiController
    {
        private readonly AllCheckedInBooksQueryHandler _checkedInBooksQueryHandler;
        private readonly CheckInCommandHandler _checkInCommandHandler;

        public CheckInController(AllCheckedInBooksQueryHandler checkedInBooksQueryHandler, CheckInCommandHandler checkInCommandHandler)
        {
            _checkedInBooksQueryHandler = checkedInBooksQueryHandler;
            _checkInCommandHandler = checkInCommandHandler;
        }

        // GET api/checkedin
        public HttpResponseMessage Get()
        {
            var booksResource = _checkedInBooksQueryHandler.Query();
            return Request.CreateResponse(HttpStatusCode.OK, booksResource);
        }

        // GET api/checkedin
        public HttpResponseMessage Post(int id)
        {
            var book = _checkInCommandHandler.Execute(id);
            if (book != null)
            {
                //yes yes, it may not have been found but not deleted. This is a demo, its ok for now
                Request.CreateErrorResponse(HttpStatusCode.NotFound, "Sorry - we could not find that book");
            }
            return Request.CreateResponse(HttpStatusCode.OK,book);
        }
    }
}