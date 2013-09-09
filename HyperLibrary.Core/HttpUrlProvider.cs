using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace HyperLibrary.Core
{
    public class HttpUrlProvider : IHttpUrlProvider
    {
        private readonly HttpConfiguration _configuration;
        private readonly Uri _currentRequestUri;

        public HttpUrlProvider(HttpConfiguration configuration, CurrentRequestUri currentRequestUri)
        {
            _configuration = configuration;
            _currentRequestUri = currentRequestUri.RequestUri;
        }

        public Uri GetBaseUrl()
        {
            var scheme = _currentRequestUri.Scheme;
            var authority = _currentRequestUri.Authority;
            var appUrl = _configuration.VirtualPathRoot;
            var url = string.Format("{0}://{1}{2}", scheme, authority, appUrl);
            return new Uri(url, UriKind.Absolute);// get the base address
        }

        //public string EncodeUrl(string urlParameter)
        //{
        //    //return _helper.Request..UrlEncode(urlParameter);
        //}

        //public string UrlDecode(string urlToDecode)
        //{
        //   // return _context.Server.UrlDecode(urlToDecode);
        //}
    }

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

    public class CurrentRequestUri
    {
        public Uri RequestUri { get; set; }

    }

}