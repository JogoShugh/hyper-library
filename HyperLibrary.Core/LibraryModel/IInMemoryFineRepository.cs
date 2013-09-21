namespace HyperLibrary.Core.LibraryModel
{
    public interface IInMemoryFineRepository
    {
        bool HasFines();
        void PayFines();
    }
}