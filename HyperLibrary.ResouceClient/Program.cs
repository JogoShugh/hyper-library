using System;
using System.Web.Http.SelfHost;
using HyperLibrary.Core;

namespace HyperLibrary.ResouceClient
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

            var d = new LibraryExplorer(ServerUri);
            d.Explore().Wait();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            host.CloseAsync().Wait();
        }
    }
}
