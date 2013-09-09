using System;

namespace HyperLibrary.WebHost.Library
{
    public class Link
    {
        public Uri Uri { get; set; }
        public string Rel { get; set; }
        public string Name { get; set; }
        public string Method { get; set; }
    }
}