using System;
using System.Threading.Tasks;
using HyperLibrary.ResouceClient.Model;

namespace HyperLibrary.ResouceClient
{
    public class LibraryExplorer
    {
        private readonly LibraryApiClient _libraryApi;
        private readonly BookBag _bookBag;

        public LibraryExplorer(Uri serverEndpoint)
        {
            _libraryApi = new LibraryApiClient(serverEndpoint);
            _bookBag = new BookBag();
        }

        public async Task Explore()
        {
            Console.WriteLine();
            Console.WriteLine("**************** Welcome to the Library ****************");
            LibaryOptions libaryOptions = OfferOptions();

            switch (libaryOptions)
            {
                case LibaryOptions.ExploreLibrary:
                    await ExploreLibrary();
                    break;
                case LibaryOptions.ViewCheckedOutBooks:
                    ViewCheckedOutBooks();
                    break;
                case LibaryOptions.ReturnBooks:
                    await ReturnBooks();
                    break;
                case LibaryOptions.LeaveLibary:
                    return;
            }
            await Explore();   
        }

        private LibaryOptions OfferOptions()
        {
            Console.WriteLine("1 - Explore the library.");
            Console.WriteLine("2 - View checked out books");
            Console.WriteLine("3 - Return books");
            Console.WriteLine("4 - Leave this place!");
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            string enteredText = Console.ReadLine();
            int option = Convert.ToInt32(enteredText); // should error handle a little better :) 
            return (LibaryOptions)option;
        }


        private async Task ExploreLibrary()
        {
            await ListLibraryBooks();

            var selectedBook = await SelectLibraryBook();

            var shouldCheckout = ShouldCheckout();
            if (shouldCheckout)
            {
                await TryCheckoutBook(selectedBook);
            }

            Console.WriteLine();
        }

        private async Task ReturnBooks()
        {
            ViewCheckedOutBooks();
            Console.WriteLine();
            Console.WriteLine("Enter the number of the book you would like to return.");
            string enteredText = Console.ReadLine();
            int bookId = Convert.ToInt32(enteredText); // should error handle a little better :) 
            await TryCheckInBook(bookId);
            Console.WriteLine();
        }

        private void ViewCheckedOutBooks()
        {
            Console.WriteLine();
            Console.WriteLine("**************** Listing All Checked out Books ****************");
            Console.WriteLine();
            foreach (Book book in _bookBag)
            {
                Console.WriteLine("{0} : '{1}' by {2}", book.Id, book.Title, book.Author);
            }
        }

        private static bool ShouldCheckout()
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to check out this book? y or n");
            string result = Console.ReadLine();
            return result == "y";
        }

        private async Task<Books> ListLibraryBooks()
        {
            Console.WriteLine();
            Console.WriteLine("**************** Listing All Library books ****************");
            var books = await _libraryApi.GetAllLibraryBooks();
            Console.WriteLine();
            foreach (Book book in books.Catalog)
            {
                Console.WriteLine("{0} : '{1}' by {2}", book.Id, book.Title, book.Author);
            }
            return books;
        }

        private async Task<Book> SelectLibraryBook()
        {
            Console.WriteLine();
            Console.WriteLine("Enter the number of the book you would like to view.");
            string enteredText = Console.ReadLine();
            int bookId = Convert.ToInt32(enteredText); // should error handle a little better :) 

            Console.WriteLine();
            Console.WriteLine("**************** View Library Book ****************");

            var selectedBook = await _libraryApi.GetSingleLibraryBook(bookId);
            Console.WriteLine("Title      : {0}", selectedBook.Title);
            Console.WriteLine("Author     : {0}", selectedBook.Author);
            Console.WriteLine("Description: {0}", selectedBook.Description);
            Console.WriteLine(selectedBook.Description);
            return selectedBook;
        }

        private async Task<bool> TryCheckoutBook(Book selectedBook)
        {
            Console.WriteLine();
            Console.WriteLine("**************** Checking out Library book ****************");
            bool successfullyCheckedout = false;
            if (_bookBag.IsNotInBookBag(selectedBook))
            {
                successfullyCheckedout = await _libraryApi.CheckoutBook(selectedBook.Id);
                if (successfullyCheckedout)
                {
                    Console.WriteLine("Checked out '{0}'.... placing it the book bag", selectedBook.Title);
                    _bookBag.AddCheckedoutBook(selectedBook);
                }
                else
                {
                    Console.WriteLine("Could not Checked out '{0}'.", selectedBook.Title);
                }   
            }
            else
            {
                Console.WriteLine("Whoops! It looks like you have already checked this book out");   
            }
            return successfullyCheckedout;

        }

        private async Task<bool> TryCheckInBook(int bookId)
        {
            Console.WriteLine();
            Console.WriteLine("**************** Checking In Library Book ****************");
            bool successfullyCheckedIn = false;
            if (_bookBag.IsInBookBag(bookId))
            {
                successfullyCheckedIn = await _libraryApi.CheckInBook(bookId);
                if (successfullyCheckedIn)
                {
                    var removedBook = _bookBag.RemoveBook(bookId);

                    Console.WriteLine("Checked in '{0}'.... removing it from the book bag", removedBook.Title);
                }
                else
                {
                    Console.WriteLine("Could not Checked in book with id '{0}'.",bookId);
                }
            }
            else
            {
                Console.WriteLine("Whoops! It looks like you dont have this book checked out!");
            }
            return successfullyCheckedIn;

        }
    }
}