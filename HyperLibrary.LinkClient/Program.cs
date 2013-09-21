using System;
using System.Web.Http.SelfHost;
using HyperLibrary.Core;

namespace HyperLibrary.LinkClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var demo = new Demo();
            demo.Go().Wait();
            //Console.WriteLine("*************** Starting the Hyper Library *****************");

            //var config = new HttpSelfHostConfiguration(ServerUri);

            //var apiServiceConfiguration = new ApiServiceConfiguration(config);
            //apiServiceConfiguration.Configure();

            //var host = new HttpSelfHostServer(config);

            //host.OpenAsync().Wait();
            //LinkExplorer d = new LinkExplorer(new Uri(ServerUri, "api"));
            //d.Explore().Wait();

            //Console.WriteLine("Press any key to exit");
            //Console.ReadLine();

            //host.CloseAsync().Wait();
        }
    }
}
