using System.Collections.Generic;

namespace HyperLibrary.Core.Resources
{
    public abstract class LinkableResource
    {
        public Link Self { get; set; }
        public List<Link> Links { get; set; }
    }
}