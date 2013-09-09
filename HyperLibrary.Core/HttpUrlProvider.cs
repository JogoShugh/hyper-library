using System;
using System.Web;

namespace HyperLibrary.Core
{
    public class HttpUrlProvider : IHttpUrlProvider
    {
        private readonly HttpContext _context;

        public HttpUrlProvider()
        {
            _context = HttpContext.Current;
        }

        public Uri GetBaseUrl()
        {
            var scheme = _context.Request.Url.Scheme;
            var authority = _context.Request.Url.Authority;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;
            var url = string.Format("{0}://{1}{2}", scheme, authority, appUrl);
            return new Uri(url, UriKind.Absolute);// get the base address
        }

        public string EncodeUrl(string urlParameter)
        {
            return _context.Server.UrlEncode(urlParameter);
        }

        public string UrlDecode(string urlToDecode)
        {
            return _context.Server.UrlDecode(urlToDecode);
        }
    }
}