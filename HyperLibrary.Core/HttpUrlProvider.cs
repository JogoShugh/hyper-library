using System;
using System.Net.Http;

namespace HyperLibrary.Core
{
    public class HttpUrlProvider : IHttpUrlProvider
    {
        private readonly Uri _currentRequestUri;

        public HttpUrlProvider(HttpRequestMessage currentRequestUri)
        {

            _currentRequestUri = currentRequestUri.RequestUri;
        }

        public Uri GetBaseUrl()
        {
            var scheme = _currentRequestUri.Scheme;
            var authority = _currentRequestUri.Authority;
            var url = string.Format("{0}://{1}", scheme, authority);
            return new Uri(url, UriKind.Absolute);// get the base address
        }
    }
}