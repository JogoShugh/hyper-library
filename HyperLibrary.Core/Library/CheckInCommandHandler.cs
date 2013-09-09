namespace HyperLibrary.Core.Library
{
    public class CheckInCommandHandler
    {
        private readonly IInMemoryBookRepository _bookRepository;
        private readonly BookResourceMapper _bookResourceMapper;

        public CheckInCommandHandler(IInMemoryBookRepository bookRepository, BookResourceMapper bookResourceMapper)
        {
            _bookRepository = bookRepository;
            _bookResourceMapper = bookResourceMapper;
        }

        public BookResource Execute(int checkIn)
        {
            var book = _bookRepository.Get(checkIn);
            book.CheckIn("fake user");
            var resource = _bookResourceMapper.MapToResouce(book);
            return resource;
        }
    }
}