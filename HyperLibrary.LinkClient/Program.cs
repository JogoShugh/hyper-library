namespace HyperLibrary.LinkClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var demo = new Demo();
            demo.Go().Wait();
        }
    }
}
