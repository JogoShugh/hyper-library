using System.Collections.Generic;

namespace HyperLibrary.Core.Library
{
    public class BookCatalogResource : LinkableResource
    {
        public IList<BookResource> Catalog { get; set; }
    }
}