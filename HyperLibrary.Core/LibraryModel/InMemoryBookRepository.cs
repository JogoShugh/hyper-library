using System.Collections.Generic;
using System.Linq;

namespace HyperLibrary.Core.LibraryModel
{
    public interface IInMemoryBookRepository
    {
        IEnumerable<Book> GetAll();
        Book Get(int bookId);
        Book Add(Book book);
        bool Delete(int bookId);
    }

    public class InMemoryFineRepository : IInMemoryFineRepository
    {
        private static bool _hasFines = true;

        public bool HasFines()
        {
            return _hasFines;
        }

        public void PayFines()
        {
            _hasFines = false;
        }
    }

    public interface IInMemoryFineRepository
    {
        bool HasFines();
        void PayFines();
    }

    public class InMemoryBookRepository : IInMemoryBookRepository
    {
        private static readonly List<Book> Books;

        static InMemoryBookRepository()
        {
            Books = new List<Book>
                        {
                            new Book{Id = 1,Title= "The Message Given To Me By Extra-Terrestrials", Author = "Claude Vorilhon 'Rael'",Description = "They Took Me To Their Planet",CoverFilename = "they-took-me-to-their-planet.jpg"},
                            new Book{Id = 2,Title= "The Little Book of Pants", Author = "Vestan Pance'",Description = @"At what point did an executive at Michael O'Mara Books Ltd say, 'let's take lots of famous quotes and insert the word PANTS into them, 'cos that will be really funny. ",CoverFilename = "the-little-book-of-pants.jpg"},
                            new Book{Id = 3,Title= "Our Mysterious Spaceship Moon", Author = "Don Wilson",Description = "Is the moon a hollowed-out spaceship sent to orbit our earth in the remote prehistoric past? Was it once inhabited by alien space travellers?",CoverFilename = "spaceship-moon.jpg"},
                            new Book{Id = 4,Title= "Look Eleven Years Younger", Author = " Gelett Burgess",Description = "You too can look not ten years, but eleven years younger!",CoverFilename = "look-eleven-years-younger.jpg"},
                            new Book{Id = 5,Title= "When you've got to go", Author = " Mitchell Kriegman",Description = "OK, so we realise that potty training is an important period when you have kids.",CoverFilename = "when-you've-got-to-go.jpg"},
                            new Book{Id = 6,Title= "Ju-Jitsu Self Defence", Author = "W. Bruce Sutherland",Description = "One of the earliest Martial Arts books to be published in the UK, this was written by the man who bought Ju-jitsu to the British Isles, Mr W. Bruce Sutherland of the Edinburgh School of Physical Culture.",CoverFilename = "ju-jitsu.jpg"}
        
                        };
        }

        static int GetNextId()
        {
            return Books.OrderBy(b => b.Id).Last().Id + 1;
        }

        public IEnumerable<Book> GetAll()
        {
            return Books;
        }
 
        public Book Get(int bookId)
        {
            return Books.FirstOrDefault(book => book.Id == bookId);
        }

        public Book Add(Book book)
        {
            book.Id = GetNextId();
            Books.Add(book);
            return book;
        }

        public bool Delete(int bookId)
        {
            return Books.Remove(Get(bookId));
        }
    }
}