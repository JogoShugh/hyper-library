using System;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using HyperLibrary.Core;
using HyperLibrary.ResouceClient;

namespace HyperLibrary.ConsoleHost
{
    class Program
    {
        private static readonly Uri ServerUri = new Uri("http://localhost:9200");
   
        static void Main(string[] args)
        {
            Console.WriteLine("*************** Starting the Hyper Library *****************");

            var config = new HttpSelfHostConfiguration(ServerUri);

            var apiServiceConfiguration = new ApiServiceConfiguration(config);
            apiServiceConfiguration.Configure();

            var host = new HttpSelfHostServer(config);

            host.OpenAsync().Wait();

            StartRegularRestDemo().Wait();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            host.CloseAsync().Wait();
        }

        private static Task StartRegularRestDemo() 
        {
           // LibraryLinkExplorer d = new LibraryLinkExplorer(new Uri(ServerUri, "api"));
            LibraryExplorer d = new LibraryExplorer(ServerUri);
            return d.Explore();
        }
    }
}
