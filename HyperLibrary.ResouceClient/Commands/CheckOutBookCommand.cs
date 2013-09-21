using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HyperLibrary.ResouceClient.Model;

namespace HyperLibrary.ResouceClient.Commands
{
    internal class CheckOutBookCommand : ICommand
    {
        private readonly BookBag _bookBag;
        private readonly Book _book;
        private readonly LibraryApiClient _libraryApiClient;

        public CheckOutBookCommand(Book book, BookBag bookBag, LibraryApiClient libraryApiClient)
        {
            _bookBag = bookBag;
            _book = book;
            _libraryApiClient = libraryApiClient;
        }

        public string Description { get { return "Check out this book"; }}

        public async Task<IEnumerable<ICommand>> Execute()
        {

            Console.WriteLine();
            Console.WriteLine("**************** Checking Out Library book ****************");
            if (_bookBag.IsNotInBookBag(_book))
            {
                bool successfullyCheckedout = await _libraryApiClient.CheckoutBook(_book.Id);
                if (successfullyCheckedout)
                {
                    Console.WriteLine("Checked out '{0}'.... placing it the book bag", _book.Title);
                    _bookBag.AddCheckedoutBook(_book);
                }
                else
                {
                    Console.WriteLine("Could not Checked out '{0}'.", _book.Title);
                    return new List<ICommand>{this};
                }
            }
            else
            {
                Console.WriteLine("Whoops! It looks like you have already checked this book out");
            }
            return new List<ICommand> { new CheckInBookCommand(_book, _bookBag, _libraryApiClient) };

        }
    }
}