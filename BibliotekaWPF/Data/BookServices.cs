using BibliotekaWPF.Models;
using BibliotekaWPF.ModelsDTO;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.Data
{
    public class BookServices
    {
        public static List<BookDTO> GetAll()
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Books.Select(
                                   x => new BookDTO
                                   {
                                       BookId = x.BookId,
                                       Title = x.Title,
                                       Author = x.Author,
                                       ReleaseDate = x.ReleaseDate,
                                       ISBN = x.ISBN,
                                       Count = x.Count,
                                       AddDate = x.AddDate,
                                       ModifiedDate = x.ModifiedDate,
                                       BookGenreId = x.BookGenreId,
                                       BookGenreName = x.DictBookGenre.Name
                                   }).ToList();
                return result;
            }
        }
        public static BindableCollection<BookDTO> GetAllBindableCollection()
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = new BindableCollection<BookDTO>(db.Books.Where(x=>x.Count>0).Select(
                                   x => new BookDTO
                                   {
                                       BookId = x.BookId,
                                       Title = x.Title,
                                       Author = x.Author,
                                       ReleaseDate = x.ReleaseDate,
                                       ISBN = x.ISBN,
                                       Count = x.Count,
                                       AddDate = x.AddDate,
                                       ModifiedDate = x.ModifiedDate,
                                       BookGenreId = x.BookGenreId,
                                       BookGenreName = x.DictBookGenre.Name
                                   }).ToList());
                return result;
            }
        }

        public static BookDTO GetOne(int id)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Books.Where(x => x.BookId == id).Select(
                                    x => new BookDTO
                                    {
                                        BookId = x.BookId,
                                        Title = x.Title,
                                        Author = x.Author,
                                        ReleaseDate = x.ReleaseDate,
                                        ISBN = x.ISBN,
                                        Count = x.Count,
                                        AddDate = x.AddDate,
                                        ModifiedDate = x.ModifiedDate,
                                        BookGenreId = x.BookGenreId,
                                        BookGenreName = x.DictBookGenre.Name
                                    }).FirstOrDefault();

                return result;
            }
        }

        public static string Add(string title, string author, DateTime? releaseDate, string isbn, int count, DictBookGenre selectedBookGenre)
        {
            
            using (LibraryContext db = new LibraryContext())
            {
                string error = null;
                Book newBook = new Book();
                newBook.Author = author;
                newBook.Title = title;
                
                //if (DateTime.TryParse(releaseDate, out dt) && dt.Year > 1753)
                //{
                //    newBook.ReleaseDate = dt;
                //}
                //else
                //    newBook.ReleaseDate = null;
                newBook.ReleaseDate = releaseDate;
                newBook.ISBN = isbn;
                newBook.Count = count;
                newBook.AddDate = DateTime.Now;
                newBook.ModifiedDate = DateTime.Now;
                newBook.BookGenreId = selectedBookGenre.BookGenreId;


                var context = new ValidationContext(newBook, null, null);
                var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                Validator.TryValidateObject(newBook, context, result, true);

                foreach (var x in result)
                {
                    error = error + x.ErrorMessage + "\n";
                }




                if (error == null)
                {
                    db.Books.Add(newBook);
                    db.SaveChanges();
                }
                return error;
             }

        }

        public static string Modify(BookDTO book)
        {



            using (LibraryContext db = new LibraryContext())
            {
                string error = null;

                var toModify = db.Books.Where(x => x.BookId == book.BookId).FirstOrDefault();

                toModify.Author = book.Author;
                toModify.Title = book.Title;
                toModify.ReleaseDate = book.ReleaseDate;
                toModify.Count = book.Count;
                toModify.ISBN = book.ISBN;
                toModify.BookGenreId = book.BookGenreId;
                toModify.ModifiedDate = DateTime.Now;
                
                var context = new ValidationContext(toModify, null, null);
                var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                Validator.TryValidateObject(toModify, context, result, true);
                
                foreach (var x in result)
                {
                    error = error + x.ErrorMessage + "\n";
                }
                                
                if (error == null)
                {
                    db.SaveChanges();
                }
                return error;
            }
        }
        public static List<BorrowDetailsDTO> GetActualBorrows(int id)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Borrows.Where(x => x.BookId == id && x.IsReturned == false).Select(
                    x => new BorrowDetailsDTO
                    {
                        UserFullName = x.User.FirstName+" "+x.User.LastName,
                        BookTitle=x.Book.Title,
                        ReturnDate = x.ToDate
                    }).ToList();

                return result;

            }
        }

        public static List<BorrowDetailsDTO> GetHistoryBorrows(int id)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Borrows.Where(x => x.BookId == id && x.IsReturned == true).Select(
                    x => new BorrowDetailsDTO
                    {
                        UserFullName = x.User.FirstName + " " + x.User.LastName,
                        BookTitle = x.Book.Title,
                        ReturnDate = x.ToDate
                    }).ToList();

                return result;

            }
        }

        public static BindableCollection<BookDTO> GetAllAvaibleBooksForUserBindableCollection(int userId)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var booksBorrowed = db.Borrows.Where(x => x.UserId == userId && x.IsReturned==false).Select(x => new BorrowDetailsDTO
                {
                    BookId = x.BookId
                }).ToList();

                BindableCollection<BookDTO> AvaibleBooksForUser = new BindableCollection<BookDTO>(db.Books.Where(x => x.Count > 0).Select(
                                   x => new BookDTO
                                   {
                                       BookId = x.BookId,
                                       Title = x.Title,
                                       Author = x.Author,
                                       ReleaseDate = x.ReleaseDate,
                                       ISBN = x.ISBN,
                                       Count = x.Count,
                                       AddDate = x.AddDate,
                                       ModifiedDate = x.ModifiedDate,
                                       BookGenreId = x.BookGenreId,
                                       BookGenreName = x.DictBookGenre.Name
                                   }).ToList());

                foreach (var item in booksBorrowed)
                {
                    var y = AvaibleBooksForUser.Where(x => x.BookId == item.BookId).FirstOrDefault();
                    AvaibleBooksForUser.Remove(y);
                }

                return AvaibleBooksForUser;
            }
        }

        public static int NumberOfBooks(string title, DictBookGenre bookGenre)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Books.Where(x => (title == null || x.Title.Contains(title)) &&
                    (bookGenre.BookGenreId == 0 || x.BookGenreId == bookGenre.BookGenreId)
                    ).Count();

                return result;
            }
        }

    }
}
