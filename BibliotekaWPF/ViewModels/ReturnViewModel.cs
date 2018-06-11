using BibliotekaWPF.Data;
using BibliotekaWPF.ModelsDTO;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.ViewModels
{
    public class ReturnViewModel :Screen
    {
        public List<BorrowDetailsDTO> BorrowedBook { get; set; } = new List<BorrowDetailsDTO>();
        public List<BorrowDetailsDTO> PickedBooksToReturn { get; set; } = new List<BorrowDetailsDTO>();
        int userId;
        public ReturnViewModel(List<BorrowDetailsDTO> clickedUser)
        {
            userId = clickedUser[0].UserId;
            BorrowedBook = BorrowServices.GetUserBorrows(userId);
        }

        public void AddToReturnList()
        {
            foreach (var item in BorrowedBook)
            {
                if (item.IsSelected == true)
                {
                    BorrowServices.ReturnOneBook(item);
                }
                OnReturnBorrowView();
            }
            
        }

        public void Cancel()
        {
            OnReturnBorrowView();
        }

        public delegate void ReturnToBorrowHandler(object source, EventArgs e);
        public event ReturnToBorrowHandler ReturnBorrowView;
        protected virtual void OnReturnBorrowView()
        {
            if (ReturnBorrowView != null)
            {
                ReturnBorrowView(this, EventArgs.Empty);
            }
        }


    }
}
