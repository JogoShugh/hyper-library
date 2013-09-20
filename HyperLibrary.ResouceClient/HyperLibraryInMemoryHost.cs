using System;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using HyperLibrary.Core;

namespace HyperLibrary.ResouceClient
{
    public class HyperLibraryInMemoryHost
    {
        private readonly Uri _serverUri;
        private HttpSelfHostServer _host;

        public HyperLibraryInMemoryHost(Uri serverUri)
        {
            _serverUri = serverUri;
        }

        public Task Start()
        {
            Console.WriteLine("*************** Starting the Hyper Library *****************");
            var selfHostConfiguration = new HttpSelfHostConfiguration(_serverUri);
            var apiServiceConfiguration = new ApiServiceConfiguration(selfHostConfiguration);
            apiServiceConfiguration.Configure();
            _host = new HttpSelfHostServer(selfHostConfiguration);
            return _host.OpenAsync();
        }

        public Task Stop()
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
            return _host.CloseAsync();
        }
    }
}