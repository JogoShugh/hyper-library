using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HyperLibrary.ResouceClient.Model;

namespace HyperLibrary.ResouceClient.Commands
{
    internal class ViewCheckedOutBooksCommand : ICommand
    {
        private readonly BookBag _bookBag;
        private readonly LibraryApiClient _libraryApiClient;

        public ViewCheckedOutBooksCommand(BookBag bookBag, LibraryApiClient libraryApiClient)
        {
            _bookBag = bookBag;
            _libraryApiClient = libraryApiClient;
        }

        public string Description { get { return "View checked out books"; } }

        public Task<IEnumerable<ICommand>> Execute()
        {
            Console.WriteLine();
            Console.WriteLine("**************** Listing All Checked out Books ****************");
            Console.WriteLine();
            IEnumerable<ICommand> commands;
            if (_bookBag.Any())
            {
                 commands = _bookBag.Select(book => new CheckInBookCommand(book, _bookBag, _libraryApiClient));
            }
            else
            {
                Console.WriteLine("No books checked out.");
                commands = new List<ICommand>{this};
            }
            return Task.FromResult(commands);
        }
    }
}