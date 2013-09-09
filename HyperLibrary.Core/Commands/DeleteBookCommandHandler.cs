using HyperLibrary.Core.LibraryModel;

namespace HyperLibrary.Core.Commands
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
}