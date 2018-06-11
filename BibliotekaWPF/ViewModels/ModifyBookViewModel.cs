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
    public class ModifyBookViewModel : Screen
    {
        private BookDTO _book = new BookDTO();
        public static BindableCollection<DictBookGenre> BookGenres { get; set; }
        private int actualGenre;


        public ModifyBookViewModel(BookDTO book)
        {
            _book = book;
            BookGenres = DictBookGenreServices.GetAll();
            actualGenre = _book.BookGenreId - 1;
    }
        
        public string Title
        {
            get { return _book.Title; }
            set { _book.Title = value; }
        }

        public string Author
        {
            get { return _book.Author; }
            set { _book.Author = value; }
        }

        public DateTime? ReleaseDate
        {
            get { return _book.ReleaseDate; }
            set { _book.ReleaseDate = value; }
        }

        public string ISBN
        {
            get { return _book.ISBN; }
            set { _book.ISBN = value; }
        }

        public int Count
        {
            get { return _book.Count; }
            set { _book.Count = value; }
        }

        public int ActualGenre
        {
            get { return actualGenre; }
            set { actualGenre = value; }
        }

        private DictBookGenre selectedBookGenre;
        public DictBookGenre SelectedBookGenre
        {
            get { return selectedBookGenre; }
            set { selectedBookGenre = value; }
        }

        string _error;

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
            _book.BookGenreId = selectedBookGenre.BookGenreId;
            string x = BookServices.Modify(_book);
            if (x == null)
                TryClose();
            else
                Error = x;
        }
    }
}
