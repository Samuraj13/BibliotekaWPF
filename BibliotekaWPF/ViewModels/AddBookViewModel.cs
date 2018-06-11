using BibliotekaWPF.Data;
using BibliotekaWPF.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.ViewModels
{
    public class AddBookViewModel : Screen
    {

        public static BindableCollection<DictBookGenre> BookGenres { get; set; }

        private Book _book = new Book();

        private DictBookGenre selectedBookGenre;

        private string _error;


        public AddBookViewModel()
        {
            BookGenres = DictBookGenreServices.GetAll();
            selectedBookGenre = BookGenres[0];
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

        private DateTime? releaseDate = null;
        public DateTime? ReleaseDate
        {
            get { return releaseDate; }
            set { releaseDate = value; }
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

        public DictBookGenre SelectedBookGenre
        {
            get { return selectedBookGenre; }
            set { selectedBookGenre = value; }
        }

        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                NotifyOfPropertyChange(() => Error);
            }
        }

        public void AddNewBook()
        {
            string x = BookServices.Add(Title, Author, ReleaseDate, ISBN, Count, SelectedBookGenre);
            if (x == null)
                TryClose();
            else
                Error = x;
        }

        public void Close()
        {
            TryClose();
        }
    }
}
