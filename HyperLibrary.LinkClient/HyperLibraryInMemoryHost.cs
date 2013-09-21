using System;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using HyperLibrary.Core;

namespace HyperLibrary.LinkClient
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

            var selfHostConfiguration = new HttpSelfHostConfiguration(_serverUri);
            var apiServiceConfiguration = new ApiServiceConfiguration(selfHostConfiguration);
            apiServiceConfiguration.Configure();
            _host = new HttpSelfHostServer(selfHostConfiguration);
            return _host.OpenAsync();
        }

        public Task Stop()
        {
            return _host.CloseAsync();
        }
    }
}