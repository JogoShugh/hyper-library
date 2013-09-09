using System.Collections.Generic;

namespace HyperLibrary.WebHost.Library
{
    public class BookCatalogResource : LinkableResource
    {
        public IList<BookResource> Catalog { get; set; }
    }
}