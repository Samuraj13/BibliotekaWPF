using BibliotekaWPF.Data;
using BibliotekaWPF.Models;
using BibliotekaWPF.ModelsDTO;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.ViewModels
{
    public class ModifyUserViewModel:Screen
    {
        public UserDTO _user = new UserDTO();

        public ModifyUserViewModel(UserDTO user)
        {
            _user = user;

        }

        public string FirstName
        {
            get { return _user.FirstName; }
            set
            {
                _user.FirstName = value;

            }
        }

        public string LastName
        {
            get { return _user.LastName; }
            set
            {
                _user.LastName = value;

            }
        }


        public DateTime? BirthDate
        {
            get { return _user.BirthTime; }
            set { _user.BirthTime = value; }
        }


        public string Email
        {
            get { return _user.Email; }
            set
            {
                _user.Email = value;

            }
        }


        public string Phone
        {
            get { return _user.Phone; }
            set
            {
                _user.Phone = value;

            }
        }

        private string _error;

        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                NotifyOfPropertyChange(() => Error);
            }
        }

        public void Modify()
        {
            string x = UserServices.Modify(_user);
            if (x == null)
                TryClose();
            else
                Error = x;
        }


        
        public void ModifyDiscard()
        {
            TryClose();
        }
    }
}
