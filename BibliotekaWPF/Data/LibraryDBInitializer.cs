using BibliotekaWPF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.Data
{
    public class LibraryDBInitializer : DropCreateDatabaseAlways<LibraryContext>
    {
        protected override void Seed(LibraryContext db)
        {
            base.Seed(db);
            IList<User> Users = new List<User>();
            DateTime dt;
            DateTime.TryParse("2/12/2012", out dt);
            Users.Add(new User() { UserId = 1, FirstName = "Tommy", LastName = "Hanks", BirthTime = dt, Email = "tom@hanks.com", Phone = "524122536", IsActive = true, AddDate = DateTime.Now, ModifiedDate = DateTime.Now });
            Users.Add(new User() { UserId = 2, FirstName = "Carol", LastName = "Cruise", BirthTime = dt, Email = "c.cruise@hoink.com", Phone = "", IsActive = true, AddDate = DateTime.Now, ModifiedDate = DateTime.Now });
            Users.Add(new User() { UserId = 3, FirstName = "Jefrey", LastName = "Small", BirthTime = dt, Email = "little.one@gmail.com", Phone = "878549654", IsActive = true, AddDate = DateTime.Now, ModifiedDate = DateTime.Now });
            Users.Add(new User() { UserId = 4, FirstName = "Dave", LastName = "Dave", BirthTime = dt, Email = "davidDave@gmail.com", Phone = "641012562", IsActive = true, AddDate = DateTime.Now, ModifiedDate = DateTime.Now });
            Users.Add(new User() { UserId = 5, FirstName = "Tonny", LastName = "Montana", BirthTime = dt, Email = "tonny.montana@king.com", Phone = "452111766", IsActive = true, AddDate = DateTime.Now, ModifiedDate = DateTime.Now });
            Users.Add(new User() { UserId = 6, FirstName = "Henry", LastName = "ChoosenOne", BirthTime = dt, Email = "im@not.here", Phone = "000111000", IsActive = false, AddDate = DateTime.Now, ModifiedDate = DateTime.Now });

            foreach (var item in Users)
                db.Users.Add(item);

            IList<DictBookGenre> BookGenres = new List<DictBookGenre>();
            BookGenres.Add(new DictBookGenre() { BookGenreId = 1, Name = "Drama" });
            BookGenres.Add(new DictBookGenre() { BookGenreId = 2, Name = "Comedy" });
            BookGenres.Add(new DictBookGenre() { BookGenreId = 3, Name = "Fantasy" });
            BookGenres.Add(new DictBookGenre() { BookGenreId = 4, Name = "History" });
            BookGenres.Add(new DictBookGenre() { BookGenreId = 5, Name = "Horror" });

            foreach (var item in BookGenres)
                db.DictBookGenres.Add(item);

            IList<Book> Books = new List<Book>();
            Books.Add(new Book() { BookId = 1, Title = "Princess", Author = "Carl Gustav", ISBN = "15246321513", ReleaseDate = null, AddDate = DateTime.Now, BookGenreId = 1, Count = 10, ModifiedDate = DateTime.Now });
            Books.Add(new Book() { BookId = 2, Title = "The King", Author = "Tonny Peperoni", ISBN = "848148184", ReleaseDate = dt, AddDate = DateTime.Now, BookGenreId = 2, Count = 210, ModifiedDate = DateTime.Now });
            Books.Add(new Book() { BookId = 3, Title = "Little Mermaid", Author = "Mironi Spaghetti", ISBN = "2634745845754", ReleaseDate = null, AddDate = DateTime.Now, BookGenreId = 3, Count = 8, ModifiedDate = DateTime.Now });
            Books.Add(new Book() { BookId = 4, Title = "Knight, who won with dragon", Author = "Jurand from Spychów ", ISBN = "14541816818", ReleaseDate = null, AddDate = DateTime.Now, BookGenreId = 4, Count = 2, ModifiedDate = DateTime.Now });
            Books.Add(new Book() { BookId = 5, Title = "Masterpiece", Author = "Kazimierz The Great", ISBN = "8786875841", ReleaseDate = null, AddDate = DateTime.Now, BookGenreId = 5, Count = 7, ModifiedDate = DateTime.Now });

            foreach (var item in Books)
                db.Books.Add(item);

            IList<Borrow> Borrows = new List<Borrow>();
            Borrows.Add(new Borrow { BorrowId = 1, BookId = 1, FromDate = DateTime.Now, IsReturned = false, UserId = 1, ToDate=DateTime.Now });
            Borrows.Add(new Borrow { BorrowId = 2, BookId = 1, FromDate = DateTime.Now, IsReturned = true, UserId = 2, ToDate = DateTime.Now });
            Borrows.Add(new Borrow { BorrowId = 3, BookId = 2, FromDate = DateTime.Now, IsReturned = false, UserId = 3, ToDate = DateTime.Now });
            Borrows.Add(new Borrow { BorrowId = 4, BookId = 3, FromDate = DateTime.Now, IsReturned = false, UserId = 4, ToDate = DateTime.Now });
            Borrows.Add(new Borrow { BorrowId = 5, BookId = 1, FromDate = DateTime.Now, IsReturned = false, UserId = 5, ToDate = DateTime.Now });
            Borrows.Add(new Borrow { BorrowId = 6, BookId = 4, FromDate = DateTime.Now, IsReturned = true, UserId = 1, ToDate = DateTime.Now });
            Borrows.Add(new Borrow { BorrowId = 7, BookId = 5, FromDate = DateTime.Now, IsReturned = true, UserId = 2, ToDate = DateTime.Now });
            Borrows.Add(new Borrow { BorrowId = 8, BookId = 1, FromDate = DateTime.Now, IsReturned = false, UserId = 3, ToDate = DateTime.Now });
            Borrows.Add(new Borrow { BorrowId = 9, BookId = 5, FromDate = DateTime.Now, IsReturned = false, UserId = 4, ToDate = DateTime.Now });
            Borrows.Add(new Borrow { BorrowId = 10, BookId = 3, FromDate = DateTime.Now, IsReturned = false, UserId = 5, ToDate = DateTime.Now });

            foreach (var item in Borrows)
                db.Borrows.Add(item);
            db.SaveChanges();

        }
    }
}
