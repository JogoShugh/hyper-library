using System;
using System.Threading.Tasks;

namespace HyperLibrary.ResouceClient
{
    public class Demo
    {
        private readonly Uri _serverUri= new Uri("http://localhost:9200");
        private readonly HyperLibraryInMemoryHost _hyperLibrary;
        private readonly LibraryExplorer _libraryExplorer;

        public Demo()
        {
            _hyperLibrary = new HyperLibraryInMemoryHost(_serverUri);
            _libraryExplorer = new LibraryExplorer(new LibraryApiClient(_serverUri));
        }

        public async Task Go()
        {
            Console.WriteLine("*************** Starting the Hyper Library *****************");
            await _hyperLibrary.Start();

            await _libraryExplorer.Explore();

            Console.WriteLine("*************** Stop the Hyper Library *********************");
            await _hyperLibrary.Stop();
        }
    }
}