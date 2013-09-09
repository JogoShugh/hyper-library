using System.Collections.Generic;
using System.Net.Http;
using HyperLibrary.WebHost.Controllers;

namespace HyperLibrary.WebHost.Library
{
    public class GetBookQueryHandler
    {
        private readonly IInMemoryBookRepository _bookRepository;
        private readonly IResourceLinker _resourceLinker;

        public GetBookQueryHandler(IInMemoryBookRepository bookRepository, IResourceLinker resourceLinker)
        {
            _bookRepository = bookRepository;
            _resourceLinker = resourceLinker;
        }

        public BookResource Query(int bookId)
        {
            var book = _bookRepository.Get(bookId);
            BookResource resource = new BookResource();
            resource.Id = book.Id;
            resource.Title = book.Title;
            resource.Author = book.Author;
            resource.Description = book.Description;
            resource.Self = _resourceLinker.GetResourceLink<BooksController>(request => request.Get(book.Id), "self", book.Title, HttpMethod.Get);
            resource.Links = new List<Link>();
            if (book.IsCheckedOut)
            {
                var checkInLink = _resourceLinker.GetResourceLink<CheckInController>(request => request.Post(book.Id), "Check In", book.Title, HttpMethod.Post);
                resource.Links.Add(checkInLink);
            }

            if (book.IsCheckedIn)
            {
                var checkoutLink = _resourceLinker.GetResourceLink<CheckedOutController>(request => request.Post(book.Id), "Check Out", book.Title, HttpMethod.Post);
                resource.Links.Add(checkoutLink);
            }
            return resource;
        }
    }
}