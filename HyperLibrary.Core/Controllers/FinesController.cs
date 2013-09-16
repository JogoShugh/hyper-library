using System.Net;
using System.Net.Http;
using System.Web.Http;
using HyperLibrary.Core.Commands;

namespace HyperLibrary.Core.Controllers
{
    public class FinesController : ApiController
    {
        private readonly PayFinesCommandHandler _payFinesCommand;

        public FinesController(PayFinesCommandHandler payFinesCommand)
        {
            _payFinesCommand = payFinesCommand;
        }

        public HttpResponseMessage Post()
        {
            var thanksResource = _payFinesCommand.Execute();
            return Request.CreateResponse(HttpStatusCode.OK,thanksResource);
        }
    }
}