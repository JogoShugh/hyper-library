using System;
using System.Net.Http;

namespace HyperLibrary.WebHost.Library
{
    public class Link
    {
        public Uri Uri { get; set; }
        public string Rel { get; set; }
        public string Name { get; set; }
        public HttpMethod Method { get; set; }
    }
}