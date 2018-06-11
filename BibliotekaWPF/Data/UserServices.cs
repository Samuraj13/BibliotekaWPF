using BibliotekaWPF.Models;
using BibliotekaWPF.ModelsDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.Data
{
    public class UserServices
    {
        public static List<UserDTO> GetAll()
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Users.Where(x=>x.IsActive).Select(
                                    x => new UserDTO
                                    {
                                        UserId = x.UserId,
                                        FirstName = x.FirstName,
                                        LastName = x.LastName,
                                        BirthTime = x.BirthTime,
                                        Email = x.Email,
                                        Phone = x.Phone,
                                        AddDate = x.AddDate,
                                        ModifiedDate = x.ModifiedDate,
                                        IsActive = x.IsActive,
                                        ActualBorrowsCount = x.Borrows.Where(y=>!y.IsReturned).Count()
                                    }).ToList();
                
                return result;
            }

           
            
        }

        public static UserDTO GetOne(int id)
        {
            using (LibraryContext db = new LibraryContext())
            {
       
                var result = db.Users.Where(x => x.UserId==id).Select(
                                    x => new UserDTO
                                    {
                                        UserId = x.UserId,
                                        FirstName = x.FirstName,
                                        LastName = x.LastName,
                                        BirthTime = x.BirthTime,
                                        Email = x.Email,
                                        Phone = x.Phone,
                                        AddDate = x.AddDate,
                                        ModifiedDate = x.ModifiedDate,
                                        IsActive = x.IsActive,
                                        ActualBorrowsCount = x.Borrows.Where(y => !y.IsReturned).Count()
                                    }).FirstOrDefault();

                return result;
            }
        }

        public static void Delete (int id)
        {
            using (LibraryContext db = new LibraryContext())
            {
            
                var result = db.Users.Where(x => x.UserId == id).FirstOrDefault();

                result.IsActive = false;

                db.SaveChanges();
                
            }
        }

        public static string Add(string firstName, string lastName, DateTime birthDate, string email, string phone)
        {
            using (LibraryContext db = new LibraryContext())
            {
                string error = null;
                User ctx = new User();
                //validation
                ctx.FirstName = firstName;
                ctx.LastName = lastName;
                ctx.BirthTime = birthDate;
                ctx.Email = email;
                ctx.Phone = phone;
                ctx.AddDate = DateTime.Now;
                ctx.ModifiedDate = DateTime.Now;
                ctx.IsActive = true;


                var context = new ValidationContext(ctx, null, null);
                var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                Validator.TryValidateObject(ctx, context, result, true);

                foreach (var x in result)
                {
                    error = error + x.ErrorMessage + "\n";
                }




                if (error == null)
                {
                    db.Users.Add(ctx);
                    db.SaveChanges();
                }
                return error;




            }

        }

        public static string Modify(UserDTO user)
        {
            
            using (LibraryContext db = new LibraryContext())
            {
                string error = null;
                var toModify = db.Users.Where(x => x.UserId == user.UserId).FirstOrDefault();

                toModify.FirstName = user.FirstName;
                toModify.LastName = user.LastName;
                toModify.BirthTime = user.BirthTime;
                toModify.Email = user.Email;
                toModify.Phone = user.Phone;
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
                var result = db.Borrows.Where(x => x.UserId == id && x.IsReturned == false).Select(
                    x => new BorrowDetailsDTO
                    {
                        BookTitle = x.Book.Title,
                        IsReturned = x.IsReturned,
                        ReturnDate = x.ToDate
                    }).ToList();

                return result;

            }
        }

        public static List<BorrowDetailsDTO> GetHistoryBorrows(int id)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = db.Borrows.Where(x => x.UserId == id && x.IsReturned == true).Select(
                    x => new BorrowDetailsDTO
                    {
                        BookTitle = x.Book.Title,
                        IsReturned = x.IsReturned,
                        ReturnDate = x.ToDate
                    }).ToList();

                return result;

            }
        }

    }

}


