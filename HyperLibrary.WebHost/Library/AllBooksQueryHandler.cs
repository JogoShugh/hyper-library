using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using HyperLibrary.WebHost.Controllers;

namespace HyperLibrary.WebHost.Library
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
            resource.Catalog = new List<BookResource>();
            foreach(var book in books)
            {
                resource.Catalog.Add(_bookResourceMapper.MapToResouce(book));
            }
            return resource;
        }
    }

    public class AllCheckedInBooksQueryHandler
    {
        private readonly IInMemoryBookRepository _bookRepository;
        private readonly BookResourceMapper _bookResourceMapper;
        private readonly IResourceLinker _resourceLinker;

        public AllCheckedInBooksQueryHandler(IInMemoryBookRepository bookRepository, BookResourceMapper bookResourceMapper, IResourceLinker resourceLinker)
        {
            _bookRepository = bookRepository;
            _bookResourceMapper = bookResourceMapper;
            _resourceLinker = resourceLinker;
        }

        public BookCatalogResource Query()
        {
            var books = _bookRepository.GetAll().Where(book => book.State == BookState.CheckedIn); ;
            BookCatalogResource resource = new BookCatalogResource();
            resource.Self = _resourceLinker.GetResourceLink<BooksController>(request => request.Get(), "self", "Library Catalog", HttpMethod.Get);
            resource.Catalog = new List<BookResource>();
            foreach (var book in books)
            {
                resource.Catalog.Add(_bookResourceMapper.MapToResouce(book));
            }
            return resource;
        }
    }
}