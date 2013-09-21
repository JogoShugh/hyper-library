namespace HyperLibrary.Core.LibraryModel
{
    public class InMemoryFineRepository : IInMemoryFineRepository
    {
        private static bool _hasFines = false;

        public bool HasFines()
        {
            return _hasFines;
        }

        public void PayFines()
        {
            _hasFines = false;
        }
    }
}