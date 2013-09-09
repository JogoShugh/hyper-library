namespace HyperLibrary.WebHost.Library
{
    public class DeleteBookCommandHandler
    {
        private readonly IInMemoryBookRepository _bookRepository;

        public DeleteBookCommandHandler(IInMemoryBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public bool Execute(int idToDelete)
        {
            return _bookRepository.Delete(idToDelete);
        }
    }

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