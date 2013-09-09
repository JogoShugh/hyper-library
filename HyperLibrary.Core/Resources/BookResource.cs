namespace HyperLibrary.Core.Resources
{
    public class BookResource : LinkableResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string CoverFilename { get; set; }
    }
}