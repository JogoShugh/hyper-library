using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HyperLibrary.ResouceClient
{
    public class LibraryApiClient
    {
        private readonly Uri _serverEndpoint;

        public LibraryApiClient(Uri serverEndPoint)
        {
            _serverEndpoint = serverEndPoint;
        }

        public async Task<bool> CheckoutBook(int bookId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _serverEndpoint;
                var result = await client.GetAsync(string.Format("/api/checkedout/{0}", bookId));
                return result.IsSuccessStatusCode;
            }
        }

        public async Task<Books> GetAllLibraryBooks()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _serverEndpoint;
                var result = await client.GetAsync("/api/books");
                return await result.Content.ReadAsAsync<Books>();
            }
        }

        public async Task<Book> GetSingleLibraryBook(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _serverEndpoint;
                var result = await client.GetAsync(string.Format("/api/books/{0}", id));
                return await result.Content.ReadAsAsync<Book>();
            }
        }


        public async Task<bool> CheckInBook(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _serverEndpoint;
                var result = await client.GetAsync(string.Format("/api/checkedin/{0}", id));
                return result.IsSuccessStatusCode;
            }
        }
    }
}