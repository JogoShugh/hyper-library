using System;
using System.Threading.Tasks;

namespace HyperLibrary.ResouceClient
{
    public class Demo
    {
        private readonly Uri _serverUri;

        public Demo(Uri serverUri)
        {
            _serverUri = serverUri;
        }

        public async Task Go()
        {
            var hyperLibrary = new HyperLibraryInMemoryHost(_serverUri);
            await hyperLibrary.Start();

            var libraryExplorer = new LibraryExplorer(_serverUri);
            await libraryExplorer.Explore();

            await hyperLibrary.Stop();
        }
    }
}