using BibliotekaWPF.Data;
using BibliotekaWPF.Models;
using BibliotekaWPF.ModelsDTO;
using BibliotekaWPF.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace BibliotekaWPF.ViewModels
{
    public class HomeViewModel : Conductor<object>
    {

        public HomeViewModel()
        {
            //Cheat();

        }

        //public async void Cheat()
        //{
        //    //co by potem się szybciej ładowało
        //    using (LibraryContext context = new LibraryContext())
        //    {
        //        var users = await context.Users.FirstOrDefaultAsync();
        //    }
        //}

        //Loading pages methods
        public void LoadUsersPage()
        {
            ActiveItem = new UsersViewModel();
            
        }
        public void LoadBooksPage()
        {
            ActiveItem = new BooksViewModel();
        }
        public void LoadBorrowsPage()
        {

            BorrowsViewModel abc = new BorrowsViewModel();
            abc.ReturnBookView += OnReturnBookView;
            
            ActiveItem = abc;
        }
        public void LoadReportsPage()
        {
            ActiveItem = new ReportsViewModel();
        }
        public void OnReturnBookView(object source, EventArgs e, List<BorrowDetailsDTO> clickedUser)
        {
            ReturnViewModel returnBook = new ReturnViewModel(clickedUser);
            returnBook.ReturnBorrowView += OnReturnBorrowView;
            ActiveItem = returnBook;
        }

        public void OnReturnBorrowView(object source, EventArgs e)
        {
            LoadBorrowsPage();
        }
    }
}
