using BibliotekaWPF.Data;
using BibliotekaWPF.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotekaWPF.Views;
using System.Data;
using System.Data.Entity;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;

namespace BibliotekaWPF.ViewModels
{
    public class AddUserViewModel : Screen
    {
        private string firstName;
        private string lastName;
        private string email;
        private string phone;
        private DateTime birthTime = DateTime.Now;

        public DateTime BirthTime
        {
            get { return birthTime; }
            set { birthTime = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;

            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;

            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;

            }
        }

        private string error;

        public string Error
        {
            get { return error; }
            set { error = value;
                NotifyOfPropertyChange(() => Error);
            }
        }


        public void Add()
        {
            string x = UserServices.Add(firstName, lastName, birthTime, email, phone);
            if (x == null)
                TryClose();
            else
                Error=x;
        }

        public void Close()
        {
            TryClose();
        }
        

    }
}
