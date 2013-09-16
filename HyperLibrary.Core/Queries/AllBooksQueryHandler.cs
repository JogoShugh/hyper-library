using System.Collections.Generic;
using System.Net.Http;
using HyperLibrary.Core.Controllers;
using HyperLibrary.Core.LibraryModel;
using HyperLibrary.Core.Resources;

namespace HyperLibrary.Core.Queries
{
    public class AllBooksQueryHandler
    {
        private readonly IInMemoryBookRepository _bookRepository;
        private readonly BookResourceMapper _bookResourceMapper;
        private readonly IResourceLinker _resourceLinker;

        public AllBooksQueryHandler(IInMemoryBookRepository bookRepository,BookResourceMapper bookResourceMapper, IResourceLinker resourceLinker)
        {
            _bookRepository = bookRepository;
            _bookResourceMapper = bookResourceMapper;
            _resourceLinker = resourceLinker;
        }

        public BookCatalogResource Query()
        {
            var books = _bookRepository.GetAll();
            BookCatalogResource resource = new BookCatalogResource();
            resource.Self = _resourceLinker.GetResourceLink<BooksController>(request => request.Get(), "self","Library Catalog", HttpMethod.Get);
            resource.Links = new List<Link>();
            resource.Links.Add(_resourceLinker.GetResourceLink<RootController>(request => request.Get(),"home","Home",HttpMethod.Get));
            resource.Catalog = new List<BookResource>();
            foreach(var book in books)
            {
                resource.Catalog.Add(_bookResourceMapper.MapToResouce(book));
            }
            return resource;
        }
    }
}