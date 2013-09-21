namespace HyperLibrary.ResouceClient
{
    class Program
    {
        private static void Main(string[] args)
        {
            var demo = new Demo();
            demo.Go().Wait();
        }
    }
}
