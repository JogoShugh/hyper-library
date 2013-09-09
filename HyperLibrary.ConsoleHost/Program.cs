using System;
using System.Web.Http.SelfHost;
using HyperLibrary.Core;

namespace HyperLibrary.ConsoleHost
{
    class Program
    {
        private const string ServiceUri = "http://localhost:9200";
   
        static void Main(string[] args)
        {
            Console.WriteLine("*************** Starting the Hyper Library *****************");

            var config = new HttpSelfHostConfiguration(new Uri(ServiceUri));

            var apiServiceConfiguration = new ApiServiceConfiguration(config);
            apiServiceConfiguration.Configure();

            var host = new HttpSelfHostServer(config);

            host.OpenAsync().Wait();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            host.CloseAsync().Wait();
        }
    }
}
