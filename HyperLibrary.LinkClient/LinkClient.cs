using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HyperLibrary.LinkClient
{
    public class LinkClient
    {
        private readonly Uri _serverEndpoint;

        public LinkClient(Uri serverEndpoint)
        {
            _serverEndpoint = serverEndpoint;
        }

        public async Task<dynamic> GetRoot()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(_serverEndpoint);
                return await result.Content.ReadAsAsync<dynamic>();
            }
        }

        public async Task<dynamic> Follow(dynamic link)
        {
            var method = link.Method.ToString();
            var uri = new Uri(link.Href.ToString());
            var httpMethod = new HttpMethod(method);
            using (var client = new HttpClient())
            {
                var result = await client.SendAsync(new HttpRequestMessage(httpMethod, uri));
                return await result.Content.ReadAsAsync<dynamic>();
            }
        }
    }
}