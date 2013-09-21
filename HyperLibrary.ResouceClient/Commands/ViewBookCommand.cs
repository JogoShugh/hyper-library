using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HyperLibrary.ResouceClient.Model;

namespace HyperLibrary.ResouceClient.Commands
{
    internal class ViewBookCommand : ICommand
    {
        private readonly Book _book;
        private readonly BookBag _bookBag;
        private readonly LibraryApiClient _libraryApiClient;

        public ViewBookCommand(Book book, BookBag bookBag,LibraryApiClient libraryApiClient)
        {
            _book = book;
            _bookBag = bookBag;
            _libraryApiClient = libraryApiClient;
        }

        public string Description
        {
            get { return String.Format("View '{0}' by {1}", _book.Title, _book.Author); }
        }

        public async Task<IEnumerable<ICommand>> Execute()
        {
            Console.WriteLine();
            Console.WriteLine("********** Viewing {0} **********", _book.Title);
            var selectedBook = await _libraryApiClient.GetSingleLibraryBook(_book.Id);
            Console.WriteLine();
            Console.WriteLine("Title      : {0}", selectedBook.Title);
            Console.WriteLine("Author     : {0}", selectedBook.Author);
            Console.WriteLine("Description: {0}", selectedBook.Description);

            return new List<ICommand>{new CheckOutBookCommand(_book,_bookBag,_libraryApiClient)};
        }
    }
}