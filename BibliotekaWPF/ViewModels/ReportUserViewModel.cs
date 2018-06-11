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
    public class ReportUserViewModel : Screen
    {
        public List<BorrowDetailsDTO> NumberOfBorrowsPerUser { get; set; }
        public ReportUserViewModel()
        {
            NumberOfBorrowsPerUser = BorrowServices.NumberOfBorrowsPerUser(SearchLastName);
            foreach (var item in NumberOfBorrowsPerUser)
            {
                item.UserFullName = item.UserFirstName + " " + item.UserLastName;
            }
        }
        private string searchLastName;

        public string SearchLastName
        {
            get { return searchLastName; }
            set { searchLastName = value;
                NumberOfBorrowsPerUser = BorrowServices.NumberOfBorrowsPerUser(SearchLastName);
                NotifyOfPropertyChange(() => NumberOfBorrowsPerUser);
            }
        }

    }
}
