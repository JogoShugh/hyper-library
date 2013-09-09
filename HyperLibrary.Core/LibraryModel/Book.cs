using System;

namespace HyperLibrary.Core.LibraryModel
{
    public class Book
    {
        public Book()
        {
            State = BookState.CheckedIn;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string CoverFilename { get; set; }
        public BookState State { get; private set; }
        public string CheckedOutTo { get; private set; }

        public bool IsCheckedOut
        {
            get { return State == BookState.CheckedOut; }
        }

        public bool IsCheckedIn
        {
            get { return State == BookState.CheckedIn; }
        }

        public void CheckOut(string checkedOutByUser)
        {
            if(State != BookState.CheckedIn)
            {
                throw new Exception(); // do better than this
            }
            State = BookState.CheckedOut;
            CheckedOutTo = checkedOutByUser;
        }

        public void CheckIn(string checkInByUser)
        {
            if (State == BookState.CheckedOut)
            {
                throw new Exception(); // do better than this
            }
            State = BookState.CheckedOut;
            CheckedOutTo = null;
        }
    }
}