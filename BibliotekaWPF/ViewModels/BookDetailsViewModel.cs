using BibliotekaWPF.Data;
using BibliotekaWPF.ModelsDTO;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BibliotekaWPF.ViewModels
{
    public class BookDetailsViewModel : Screen
    {

        public BookDTO _book = new BookDTO();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public List<BorrowDetailsDTO> ActualBorrows { get; set; } = new List<BorrowDetailsDTO>();
        public List<BorrowDetailsDTO> HistoryBorrows { get; set; } = new List<BorrowDetailsDTO>();

        public BookDetailsViewModel(BookDTO book)
        {
            _book = book;
            ActualBorrows = BookServices.GetActualBorrows(book.BookId);
            HistoryBorrows = BookServices.GetHistoryBorrows(book.BookId);
            dispatcherTimer.Tick += new EventHandler(LoadBooks);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(10);
            dispatcherTimer.Start();
        }


        public void LoadBooks(object sender, EventArgs e)
        {
            ActualBorrows = BookServices.GetActualBorrows(_book.BookId);
            HistoryBorrows = BookServices.GetHistoryBorrows(_book.BookId);
            NotifyOfPropertyChange(() => ActualBorrows);
            NotifyOfPropertyChange(() => HistoryBorrows);
        }

        public string Author
        {
            get { return _book.Author; }
        }

        public string Title
        {
            get { return _book.Title; }
        }


        public DateTime? ReleaseDate
        {
            get { return _book.ReleaseDate; }
        }



        public string ISBN
        {
            get { return _book.ISBN; }
        }


        public string Genre
        {
            get { return _book.BookGenreName; }
        }
        public int Count
        {
            get { return _book.Count; }
        }

        public void DetailsClose()
        {
            dispatcherTimer.Stop();
            TryClose();
        }
    }
}
