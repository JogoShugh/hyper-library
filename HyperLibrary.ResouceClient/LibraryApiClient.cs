using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HyperLibrary.ResouceClient
{
    public class LibraryApiClient
    {
        private readonly Uri _serverEndpoint;

        /// <summary>
        /// A list of relative uris that this client interacts with. 
        /// </summary>
        private const string ListBooksRelativeUri = "api/books";
        private const string GetBookRelativeUri = "/api/books/{0}";
        private const string CheckOutBookRelativeUri = "/api/checkedout/{0}";
        private const string CheckInBookRelativeUri = "/api/checkin/{0}";

        public LibraryApiClient(Uri serverEndPoint)
        {
            _serverEndpoint = serverEndPoint;
        }

        public async Task<bool> CheckoutBook(int bookId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _serverEndpoint;
                var result = await client.GetAsync(string.Format(CheckOutBookRelativeUri, bookId));
                return result.IsSuccessStatusCode;
            }
        }

        public async Task<Books> GetAllLibraryBooks()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _serverEndpoint;
                var result = await client.GetAsync(ListBooksRelativeUri);
                return await result.Content.ReadAsAsync<Books>();
            }
        }

        public async Task<Book> GetSingleLibraryBook(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _serverEndpoint;
                var result = await client.GetAsync(string.Format(GetBookRelativeUri, id));
                return await result.Content.ReadAsAsync<Book>();
            }
        }

        public async Task<bool> CheckInBook(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _serverEndpoint;
                var result = await client.GetAsync(string.Format(CheckInBookRelativeUri, id));
                return result.IsSuccessStatusCode;
            }
        }
    }
}