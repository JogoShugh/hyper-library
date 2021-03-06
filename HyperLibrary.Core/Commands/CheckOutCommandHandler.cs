using System.Collections.Generic;
using HyperLibrary.Core.LibraryModel;
using HyperLibrary.Core.Resources;

namespace HyperLibrary.Core.Commands
{
    public class CheckOutCommandHandler
    {
        private readonly IInMemoryBookRepository _bookRepository;
        private readonly BookResourceMapper _bookResourceMapper;

        public CheckOutCommandHandler(IInMemoryBookRepository bookRepository, BookResourceMapper bookResourceMapper)
        {
            _bookRepository = bookRepository;
            _bookResourceMapper = bookResourceMapper;
        }

        public BookResource Execute(int checkIn)
        {
            var book = _bookRepository.Get(checkIn);
            var links = new List<Link>();
            try
            {
                book.CheckOut("fake user");
            }
            catch (AlreadyCheckedOutException)
            {
                //log things
            }
            var resource = _bookResourceMapper.MapToResouce(book);
            resource.Links.AddRange(links);
            return resource;
        }
    }
}