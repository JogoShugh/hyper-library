using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HyperLibrary.Core
{
    /// <summary>
    /// Make the currently excuting request injectable to services. 
    /// </summary>
    public class CurrentRequestResolver : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var scope = request.GetDependencyScope();
            var currentRequest = (CurrentRequestUri)scope.GetService(typeof(CurrentRequestUri));
            currentRequest.RequestUri = request.RequestUri;
            return base.SendAsync(request, cancellationToken);
        }
    }
}