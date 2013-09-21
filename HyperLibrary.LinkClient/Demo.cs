using System;
using System.Threading.Tasks;

namespace HyperLibrary.LinkClient
{
    public class Demo
    {
        private readonly Uri _serverUri= new Uri("http://localhost:9200");
        private readonly HyperLibraryInMemoryHost _hyperLibrary;
        private readonly LinkExplorer _explorer;

        public Demo()
        {
            _hyperLibrary = new HyperLibraryInMemoryHost(_serverUri);
            _explorer = new LinkExplorer(new Uri(_serverUri,"/api"));
        }

        public async Task Go()
        {
            Console.WriteLine("*************** Starting the Hyper Library *****************");
            await _hyperLibrary.Start();

            await _explorer.Explore();

            Console.WriteLine("*************** Stop the Hyper Library *********************");
            await _hyperLibrary.Stop();
        }
    }
}