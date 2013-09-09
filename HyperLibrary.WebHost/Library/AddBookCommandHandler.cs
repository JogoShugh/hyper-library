namespace HyperLibrary.WebHost.Library
{
    public class AddBookCommandHandler
    {
        private readonly IInMemoryBookRepository _bookRepository;
        private readonly BookResourceMapper _bookResourceMapper;

        public AddBookCommandHandler(IInMemoryBookRepository bookRepository, BookResourceMapper bookResourceMapper)
        {
            _bookRepository = bookRepository;
            _bookResourceMapper = bookResourceMapper;
        }
        public BookResource Execute(Book bookToAdd)
        {
            var book = _bookRepository.Add(bookToAdd);
            var resource = _bookResourceMapper.MapToResouce(book);
            return resource;
        }
    }
}