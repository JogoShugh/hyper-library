using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HyperLibrary.Core.Controllers
{
    public class FeeController : ApiController
    {
        public HttpResponseMessage Post()
        {
            // pay the fee off
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}