using System.Collections.Generic;
using System.Net.Http;
using HyperLibrary.Core.Controllers;
using HyperLibrary.Core.Resources;

namespace HyperLibrary.Core.Queries
{
    public class GetLibraryRootHandler
    {
        private readonly IResourceLinker _linker;

        public GetLibraryRootHandler(IResourceLinker linker)
        {
            _linker = linker;
        }

        public Library Query()
        {
            var library = new Library();
            library.Name = "Hello World - Hypermedia Library";
            library.Self = _linker.GetResourceLink<RootController>(request => request.Get(), "self", "Library Options", HttpMethod.Get);
            library.Links = new List<Link>();
            library.Links.Add(_linker.GetResourceLink<BooksController>(request => request.Get(),"Books","All library books",HttpMethod.Get));
            library.Links.Add(_linker.GetResourceLink<CheckInController>(request => request.Get(),"CheckedIn","Avalible books",HttpMethod.Get));
            library.Links.Add(_linker.GetResourceLink<CheckedOutController>(request => request.Get(),"CheckedOut","Checked out books",HttpMethod.Get));
            return library;
        }
    }
}
