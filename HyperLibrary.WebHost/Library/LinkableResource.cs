using System.Collections.Generic;

namespace HyperLibrary.WebHost.Library
{
    public abstract class LinkableResource
    {
        public Link Self { get; set; }
        public IList<Link> Links { get; set; }
    }
}