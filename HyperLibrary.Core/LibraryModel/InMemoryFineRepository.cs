namespace HyperLibrary.Core.LibraryModel
{
    public class InMemoryFineRepository : IInMemoryFineRepository
    {
        /// <summary>
        /// Switch me to 'true' enable fines
        /// </summary>
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