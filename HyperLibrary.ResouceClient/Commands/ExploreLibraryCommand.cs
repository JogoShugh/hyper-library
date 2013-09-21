using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HyperLibrary.ResouceClient.Model;

namespace HyperLibrary.ResouceClient.Commands
{
    internal class ExploreLibraryCommand : ICommand
    {
        private readonly LibraryApiClient _libraryApiClient;
        private readonly BookBag _bookBag;

        public ExploreLibraryCommand(LibraryApiClient libraryApiClient,BookBag bookBag)
        {
            _libraryApiClient = libraryApiClient;
            _bookBag = bookBag;
        }

        public string Description { get { return "Explore the library."; } }

        public async Task<IEnumerable<ICommand>> Execute()
        {
            Books books = await ListLibraryBooks();
            return books.Catalog.Select(book => new ViewBookCommand(book, _bookBag,_libraryApiClient));
        }

        private Task<Books> ListLibraryBooks()
        {
            Console.WriteLine();
            Console.WriteLine("**************** Getting All Library books ****************");
            return _libraryApiClient.GetAllLibraryBooks();
        }
    }
}