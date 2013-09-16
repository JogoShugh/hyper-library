using System;

namespace HyperLibrary.Core.LibraryModel
{
    public class AlreadyCheckedOutException : Exception
    {
        public AlreadyCheckedOutException(Book book) : base(string.Format("Book {0} is already checked out", book.Id)){}
    }

    public class AlreadyCheckedInException : Exception
    {
        public AlreadyCheckedInException(Book book) : base(string.Format("Book {0} is already checked in", book.Id)) { }
    }
}