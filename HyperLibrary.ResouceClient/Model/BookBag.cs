using System.Collections.Generic;
using System.Linq;

namespace HyperLibrary.ResouceClient.Model
{
    public class BookBag : List<Book>
    {
        public bool IsNotInBookBag(Book book)
        {
            return this.All(checkOutBook => checkOutBook.Id != book.Id);
        }

        public bool IsInBookBag(int bookId)
        {
            return this.Any(book => book.Id == bookId);
        }

        public void AddCheckedoutBook(Book selectedBook)
        {
            Add(selectedBook);
        }

        public Book RemoveBook(int bookId)
        {
            Book bookToRemove = this.FirstOrDefault(book => book.Id == bookId);
            Remove(bookToRemove);
            return bookToRemove;
        }
    }
}