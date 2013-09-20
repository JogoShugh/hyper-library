using System;
using System.Web.Http.SelfHost;
using HyperLibrary.Core;

namespace HyperLibrary.LinkClient
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
            LibraryLinkExplorer d = new LibraryLinkExplorer(new Uri(ServerUri, "api"));
            d.Explore().Wait();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            host.CloseAsync().Wait();
        }

     
    }
}
