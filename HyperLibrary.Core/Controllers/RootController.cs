using System.Net;
using System.Net.Http;
using System.Web.Http;
using HyperLibrary.Core.Queries;

namespace HyperLibrary.Core.Controllers
{
    public class RootController : ApiController
    {
        private readonly GetLibraryRootHandler _rootHandler;

        public RootController(GetLibraryRootHandler rootHandler)
        {
            _rootHandler = rootHandler;
        }

        public HttpResponseMessage Get()
        {
            var library = _rootHandler.Query();
            return Request.CreateResponse(HttpStatusCode.OK, library);
        }
    }
}
