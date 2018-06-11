using BibliotekaWPF.Data;
using BibliotekaWPF.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BibliotekaWPF.ModelsDTO;

namespace BibliotekaWPF.ViewModels
{
    public class BooksViewModel : Screen
    {
        public List<BookDTO> Books { get; set; } = new List<BookDTO>();
        public BooksViewModel()
        {
            Reload();
        }
                
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

        }

        public void LoadAddBookPage()
        {
            IWindowManager manager = new WindowManager();
            AddBookViewModel add = new AddBookViewModel();
            manager.ShowDialog(add, null, null);
            Reload();
        }

        public void LoadModifyBookPage(BookDTO book)
        {
            IWindowManager manager = new WindowManager();
            ModifyBookViewModel modify = new ModifyBookViewModel(book);
            manager.ShowDialog(modify, null, null);
            Reload();
        }

        public void LoadDetailsBookPage(BookDTO book)
        {

            IWindowManager manager = new WindowManager();
            BookDetailsViewModel details = new BookDetailsViewModel(book);
            manager.ShowDialog(details, null, null);
            Reload();
        }

        public void Reload()
        {
            Books = BookServices.GetAll();
            NotifyOfPropertyChange(() => Books);
        }
        
    }
}
