using BibliotekaWPF.Data;
using BibliotekaWPF.Models;
using BibliotekaWPF.ModelsDTO;
using Caliburn.Micro;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.ViewModels

{
    public class UserDetailsViewModel : Screen
    {
        public UserDTO _user = new UserDTO();


        public List<BorrowDetailsDTO> ActualBorrows { get; set; } = new List<BorrowDetailsDTO>();
        public List<BorrowDetailsDTO> HistoryBorrows { get; set; } = new List<BorrowDetailsDTO>();

        public UserDetailsViewModel(UserDTO user)
        {
            _user = user;
            ActualBorrows = UserServices.GetActualBorrows(user.UserId);
            HistoryBorrows = UserServices.GetHistoryBorrows(user.UserId);
        }
 

        public string FirstName
        {
            get { return _user.FirstName; }
        }

        public string LastName
        {
            get { return _user.LastName; }
        }


        public DateTime? BirthTime
        {
            get { return _user.BirthTime; }
        }



        public string Email
        {
            get { return _user.Email; }
        }


        public string Phone
        {
            get { return _user.Phone; }
        }
        

        public void DetailsClose()
        {
            TryClose();
        }
    }
}
