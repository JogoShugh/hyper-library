using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HyperLibrary.ResouceClient.Model;

namespace HyperLibrary.ResouceClient.Commands
{
    internal class CheckInBookCommand : ICommand
    {
        private readonly BookBag _bookBag;
        private readonly Book _book;
        private readonly LibraryApiClient _libraryApiClient;

        public CheckInBookCommand(Book book, BookBag bookBag, LibraryApiClient libraryApiClient)
        {
            _bookBag = bookBag;
            _book = book;
            _libraryApiClient = libraryApiClient;
        }

        public string Description { get { return string.Format("Check in {0}",_book.Title); } }

        public async Task<IEnumerable<ICommand>> Execute()
        {
            Console.WriteLine();
            Console.WriteLine("**************** Checking In Library Book ****************");
            if (_bookBag.IsInBookBag(_book.Id))
            {
                bool successfullyCheckedIn = await _libraryApiClient.CheckInBook(_book.Id);
                if (successfullyCheckedIn)
                {
                    var removedBook = _bookBag.RemoveBook(_book.Id);

                    Console.WriteLine("Checked in '{0}'.... removing it from the book bag", removedBook.Title);
                }
                else
                {
                    Console.WriteLine("Could not check in book with id '{0}'.", _book.Id);
                    return new List<ICommand> { this };
                }
            }
            else
            {
                Console.WriteLine("Whoops! It looks like you dont have this book checked out!");
            }
            return new List<ICommand> { new CheckOutBookCommand(_book, _bookBag, _libraryApiClient) };
        }
    }
}