using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotekaWPF.Models;
using BibliotekaWPF.ModelsDTO;

namespace BibliotekaWPF.Data
{
    public class BorrowServices
    {
        public static List<List<BorrowDetailsDTO>> GetAllActualBorrowsGroupedByBookTitle()
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Borrows.Where(x => x.IsReturned == false).Select(
                    x => new BorrowDetailsDTO
                    {
                        BookId = x.BookId,
                        BookTitle = x.Book.Title,
                        ReturnDate = x.ToDate
                    });
                var data = result.GroupBy(x => new { x.BookId }).Select(g => g.ToList()).ToList();

                return data;

            }
        }

        public static List<BorrowDetailsDTO> GetAllActualBorrows()
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Borrows.Where(x => x.IsReturned == false).Select(
                    x => new BorrowDetailsDTO
                    {
                        BorrowId=x.BorrowId,
                        BookId = x.BookId,
                        BookTitle = x.Book.Title,
                        UserId=x.UserId,
                        UserFullName = x.User.FirstName+" "+x.User.LastName,
                        ReturnDate = x.ToDate
                    }).ToList();
                

                return result;

            }
        }

        public static List<List<BorrowDetailsDTO>> GetAllActualBorrowsGroupedByUserName()
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Borrows.Where(x => x.IsReturned == false).Select(
                    x => new BorrowDetailsDTO
                    {
                        UserId=x.UserId,
                        UserFullName = x.User.FirstName + " " + x.User.LastName,
                        ReturnDate = x.ToDate
                    });
                var data = result.GroupBy(x => new { x.UserId }).Select(g => g.ToList()).ToList();

                return data;

            }
        }

        public static string AddNewBorrow(int userId, int bookId)
        {
            using (LibraryContext db = new LibraryContext())
            {
                string error = null;
                Borrow newBorrow = new Borrow
                {
                    //validation
                    UserId = userId,
                    BookId = bookId,
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now.AddDays(14),
                    IsReturned = false
                };
                var bookCountChange = db.Books.Where(x => x.BookId == bookId).FirstOrDefault();
                bookCountChange.Count = bookCountChange.Count - 1;
                

                var context = new ValidationContext(newBorrow, null, null);
                var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                Validator.TryValidateObject(newBorrow, context, result, true);

                foreach (var x in result)
                {
                    error = error + x.ErrorMessage + "\n";
                }




                if (error == null)
                {
                    db.Borrows.Add(newBorrow);
                    db.SaveChanges();
                }
                return error;



            }
        }

        public static void ReturnOneBook(BorrowDetailsDTO borrow)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Borrows.Where(x => x.BorrowId == borrow.BorrowId).FirstOrDefault();
                result.IsReturned = true;
                result.ToDate = DateTime.Now;

                var returnedBookCount = db.Books.Where(x => x.BookId == borrow.BookId).FirstOrDefault();
                returnedBookCount.Count++;

                db.SaveChanges();

            }
        }

        public static List<BorrowDetailsDTO> GetUserBorrows(int id)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Borrows.Where(x => x.UserId == id && x.IsReturned==false).Select(x => new BorrowDetailsDTO
                {
                    BorrowId=x.BorrowId,
                    BookTitle = x.Book.Title,
                    BookAuthor = x.Book.Author,
                    BookId = x.BookId,
                    ReturnDate = x.ToDate,
                    UserId = x.UserId,
                    IsSelected=false,
                    IsReturned=x.IsReturned

                }).ToList();

                return result;
            }
        }

        public static List<BorrowDetailsDTO> NumberOfBorrowsPerUser(string lastName)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Users.Where(x=>x.IsActive && (lastName==null || x.LastName.Contains(lastName))).Select(x => new BorrowDetailsDTO
                {
                    UserFullName= x.FirstName + " " + x.LastName,
                    UserFirstName=x.FirstName,
                    UserLastName=x.LastName,
                    BorrowsCount=x.Borrows.Count()
                }).OrderByDescending(x => x.BorrowsCount).ToList();

                return result;
            }
        }

        public static List<BorrowDetailsDTO> NumberOfBorrowsPerTitle(string title, DictBookGenre bookGenre, DateTime? fromDate, DateTime? toDate, int actualPage, int pageSize)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Books.
                    Where(x => (title == null || x.Title.Contains(title)) &&
                    (bookGenre.BookGenreId == 0 || x.BookGenreId == bookGenre.BookGenreId)
                    ).Select(x => new BorrowDetailsDTO
                    {
                        BookTitle = x.Title,
                        BookAuthor = x.Author,
                        BookGenre = x.DictBookGenre.Name,
                        BorrowsCount = x.Borrow.Where(y => (fromDate == null || y.FromDate >= fromDate) &&
                    (toDate == null || y.ToDate <= toDate)).Count(),
                    }).OrderByDescending(x => x.BorrowsCount).Skip(actualPage*pageSize).Take(pageSize).ToList();

                return result;

            }

        }


    }
}
