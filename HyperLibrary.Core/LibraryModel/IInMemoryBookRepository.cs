using System.Collections.Generic;

namespace HyperLibrary.Core.LibraryModel
{
    public interface IInMemoryBookRepository
    {
        IEnumerable<Book> GetAll();
        Book Get(int bookId);
        Book Add(Book book);
        bool Delete(int bookId);
    }
}