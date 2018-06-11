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
    public class ReportBookViewModel : Screen
    {
        public List<BorrowDetailsDTO> NumberOfBorrowsPerTitle { get; set; } = new List<BorrowDetailsDTO>();
        public static BindableCollection<DictBookGenre> BookGenres { get; set; } = new BindableCollection<DictBookGenre>();
        int numberOfBooks;



        public ReportBookViewModel()
        {
            BookGenres = DictBookGenreServices.GetAll();
            DictBookGenre filterAll = new DictBookGenre() { BookGenreId = 0, Name = "All" };
            BookGenres.Add(filterAll);
            BookGenres.Move(BookGenres.IndexOf(filterAll),0);
            numberOfBooks = BookServices.NumberOfBooks(null, new DictBookGenre() { BookGenreId = 0, Name = null });
            NumberOfBorrowsPerTitle = BorrowServices.NumberOfBorrowsPerTitle(SelectedTitle, SelectedBookGenre, FromDate, ToDate, ActualPage, PageSize);
            
        }
        private DictBookGenre selectedBookGenre = new DictBookGenre() { BookGenreId = 0, Name = "No filter" };

        public DictBookGenre SelectedBookGenre
        {
            get { return selectedBookGenre; }
            set { selectedBookGenre = value; }
        }

        private string selectedTitle;

        public string SelectedTitle
        {
            get { return selectedTitle; }
            set { selectedTitle = value; }
        }

        private DateTime? fromDate;

        public DateTime? FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }

        private DateTime? toDate;

        public DateTime? ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }

        private DictBookGenre toSendSelectedBookGenre = new DictBookGenre() { BookGenreId = 0, Name = "No filter" };
        public DictBookGenre ToSendSelectedBookGenre
        {
            get { return toSendSelectedBookGenre; }
            set { toSendSelectedBookGenre = value; }
        }

        private string toSendSelectedTitle;

        public string ToSendSelectedTitle
        {
            get { return toSendSelectedTitle; }
            set { toSendSelectedTitle = value; }
        }

        private DateTime? toSendFromDate;

        public DateTime? ToSendFromDate
        {
            get { return toSendFromDate; }
            set { toSendFromDate = value; }
        }

        private DateTime? toSendToDate;

        public DateTime? ToSendToDate
        {
            get { return toSendToDate; }
            set { toSendToDate = value; }
        }

        public void Filter()
        {
            ActualPage = 0;
            ToSendFromDate = FromDate;
            ToSendToDate = ToDate;
            ToSendSelectedBookGenre = SelectedBookGenre;
            ToSendSelectedTitle = SelectedTitle;
            NotifyOfPropertyChange(() => ShowActualPage);
            numberOfBooks = BookServices.NumberOfBooks(ToSendSelectedTitle, ToSendSelectedBookGenre);
            NumberOfBorrowsPerTitle = BorrowServices.NumberOfBorrowsPerTitle(ToSendSelectedTitle, ToSendSelectedBookGenre, ToSendFromDate, ToSendToDate, ActualPage, PageSize);
            NotifyOfPropertyChange(() => NumberOfBorrowsPerTitle);
        }

        public void Reset()
        {
            ToSendSelectedTitle = null;
            ToSendSelectedBookGenre = BookGenres[0];
            ToSendFromDate = null;
            ToSendToDate = null;
            numberOfBooks = BookServices.NumberOfBooks(null, new DictBookGenre() { BookGenreId = 0, Name = null });
            NumberOfBorrowsPerTitle = BorrowServices.NumberOfBorrowsPerTitle(null, new DictBookGenre() {BookGenreId=0,Name=null }, null, null, ActualPage, PageSize);
            NotifyOfPropertyChange(() => NumberOfBorrowsPerTitle);
        }


        //Paging
        private int actualPage = 0;
        private int pageSize = 2;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value;
                ActualPage = 0;
                NotifyOfPropertyChange(() => ShowActualPage);
                NumberOfBorrowsPerTitle = BorrowServices.NumberOfBorrowsPerTitle(ToSendSelectedTitle, ToSendSelectedBookGenre, ToSendFromDate, ToSendToDate, ActualPage, PageSize);
                NotifyOfPropertyChange(() => NumberOfBorrowsPerTitle);
            }
        }

        public int ActualPage
        {
            get { return actualPage; }
            set { actualPage = value; }
        }
        
        public int ShowActualPage { get { return ActualPage+1; } }

        public void NextPage()
        {
            if ((ActualPage+1) * PageSize < numberOfBooks)
            {
                ActualPage++;
                NotifyOfPropertyChange(() => ShowActualPage);
                NumberOfBorrowsPerTitle = BorrowServices.NumberOfBorrowsPerTitle(ToSendSelectedTitle, ToSendSelectedBookGenre, ToSendFromDate, ToSendToDate, ActualPage, PageSize);
                NotifyOfPropertyChange(() => NumberOfBorrowsPerTitle);
            }
        }
        public void PreviousPage()
        {
            if (ActualPage > 0)
            {
                ActualPage--;
                NotifyOfPropertyChange(() => ShowActualPage);
                NumberOfBorrowsPerTitle = BorrowServices.NumberOfBorrowsPerTitle(ToSendSelectedTitle, ToSendSelectedBookGenre, ToSendFromDate, ToSendToDate, ActualPage, PageSize);
                NotifyOfPropertyChange(() => NumberOfBorrowsPerTitle);
            }
        }

    }
}
