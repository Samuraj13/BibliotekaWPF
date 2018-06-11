using BibliotekaWPF.Data;
using BibliotekaWPF.Models;
using BibliotekaWPF.ModelsDTO;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.ViewModels
{
    public class BorrowsViewModel : Screen
    {
        public List<BorrowDetailsDTO> AllActualBorrows { get; set; } = new List<BorrowDetailsDTO>();
        public List<List<BorrowDetailsDTO>> AllActualUsers { get; set; } = new List<List<BorrowDetailsDTO>>();
        public BorrowsViewModel()
        {
            Reload();
        }

        public void LoadAddBorrowPage()
        {
            IWindowManager manager = new WindowManager();
            AddBorrowViewModel add = new AddBorrowViewModel();
            manager.ShowDialog(add, null, null);
            Reload();
        }

        public void ReturnBook(BorrowDetailsDTO borrow)
        {
            BorrowServices.ReturnOneBook(borrow);
            Reload();
        }

        public void Reload()
        {
            AllActualBorrows = BorrowServices.GetAllActualBorrows();
            AllActualUsers = BorrowServices.GetAllActualBorrowsGroupedByUserName();
            NotifyOfPropertyChange(() => AllActualBorrows);
            NotifyOfPropertyChange(() => AllActualUsers);
        }

        public delegate void ReturnBookHandler(object source, EventArgs e, List<BorrowDetailsDTO> clickedUser);
        public event ReturnBookHandler ReturnBookView;
        protected virtual void OnReturnBookView(List<BorrowDetailsDTO> clickedUser)
        {
            if (ReturnBookView != null)
            {
                ReturnBookView(this, EventArgs.Empty, clickedUser);
            }
        }

        public void LoadReturnPage(List<BorrowDetailsDTO> clickedUser)
        {
            //List<BorrowDetailsDTO> neew = new List<BorrowDetailsDTO>(clickedUser);
            OnReturnBookView(clickedUser);
        }
    }
}
