using System;

namespace HyperLibrary.ResouceClient
{
    class Program
    {
        private static readonly Uri ServerUri = new Uri("http://localhost:9200");

        private static void Main(string[] args)
        {
            var demo = new Demo(ServerUri);
            demo.Go().Wait();
        }
    }
}
