using BibliotekaWPF.Data;
using BibliotekaWPF.ModelsDTO;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.ViewModels
{

    public class AddBorrowViewModel : Screen
    {
        public class NewBook : Screen
        {
            public BindableCollection<BookDTO> Books { get; set; }
            public delegate void SelectedBookChangeEventHandler(object source, EventArgs e, BookDTO book);
            public event SelectedBookChangeEventHandler SelectedBookChangeRemove;
            public event SelectedBookChangeEventHandler SelectedBookChangeAdd;
            int i = 0;

            public string BookNumber
            {
                get;
                set;
            }
            private BookDTO selectedBook;
            
            public BookDTO SelectedBook
            {

                get
                {
                    NotifyOfPropertyChange(() => Books);
                    i = 0;
                    return selectedBook;
                }
                set
                {
                    if (selectedBook != null && selectedBook!=value && i==0)
                    {
                        i++;
                        OnSelectedBookChangeAdd(this.selectedBook);
                    }
                    selectedBook = value;
                    OnSelectedBookChangeRemove(this.selectedBook);  
                }

                
            }
            
            protected virtual void OnSelectedBookChangeRemove(BookDTO book)
            {
                if (SelectedBookChangeRemove != null)
                {
                    SelectedBookChangeRemove(this,EventArgs.Empty, book);
                }
            }
            protected virtual void OnSelectedBookChangeAdd(BookDTO book)
            {
                if (SelectedBookChangeAdd != null)
                {
                    SelectedBookChangeAdd(this, EventArgs.Empty, book);
                    
                }
                
            }

        }


        public List<UserDTO> Users { get; set; }
        public List<BookDTO> AllBooks { get; set; } = new List<BookDTO>();
        public BindableCollection<BookDTO> AvaibleBooks { get; set; } = new BindableCollection<BookDTO>();
        int numberOfAvaibleBooks;
        public ObservableCollection<NewBook> BorrowsList { get; set; }
        public List<BookDTO> PickedBooks { get; set; } = new List<BookDTO>();

        public AddBorrowViewModel()
        {
            Users = UserServices.GetAll();
            AllBooks = BookServices.GetAll();

            BorrowsList = new ObservableCollection<NewBook>();
        }

        public void OnSelectedBookChangeRemove(object source, EventArgs e, BookDTO book)
        {
            AvaibleBooks.Remove(book);
            PickedBooks.Add(book);
            foreach (var item in BorrowsList)
            {
                if (item.SelectedBook != book)
                {
                    item.Books.Remove(book);
                }
            }
        }
        public void OnSelectedBookChangeAdd(object source, EventArgs e, BookDTO book)
        {
            AvaibleBooks.Add(book);
            PickedBooks.Remove(book);
            foreach (var item in BorrowsList)
            {
                if (item.SelectedBook != book)
                {
                    item.Books.Add(book);
                }
            }
        }
        int i = 1;
        public void AddNewBorrow()
        {
           
            NewBook m = new NewBook();
            m.BookNumber = "Book " + i.ToString();
            BorrowsList.Add(m);
            m.Books = new BindableCollection<BookDTO>(AvaibleBooks);
            m.SelectedBookChangeRemove += OnSelectedBookChangeRemove;
            m.SelectedBookChangeAdd += OnSelectedBookChangeAdd;
            NotifyOfPropertyChange(() => CanAdd);
            i++;
        }

        private UserDTO selectedUser;

        public UserDTO SelectedUser
        {
            get {
                return selectedUser; }
            set {
                selectedUser = value;
                BorrowsList.Clear();
                AvaibleBooks = BookServices.GetAllAvaibleBooksForUserBindableCollection(SelectedUser.UserId);
                i = 1;
                numberOfAvaibleBooks = AvaibleBooks.Count();
                NotifyOfPropertyChange(() => AvaibleBooks);
                    NotifyOfPropertyChange(() => CanAdd);
            }
        }

        public void Remove(NewBook book)
        {
            if (book.SelectedBook != null)
            {
                foreach (var item in BorrowsList)
                {
                    item.Books.Add(book.SelectedBook);
                }
                AvaibleBooks.Add(book.SelectedBook);
            }
            BorrowsList.Remove(book);
            int number = 1;
            foreach (var item in BorrowsList)
            {
                item.BookNumber = "Book " + number.ToString();
            }
            number = 1;
            NotifyOfPropertyChange(() => CanAdd);
            NotifyOfPropertyChange(() => BorrowsList);
        }

        public void AcceptBorrows()
        {
            foreach (var item in BorrowsList)
            {
                if(item.SelectedBook!=null)
                    BorrowServices.AddNewBorrow(SelectedUser.UserId, item.SelectedBook.BookId);
            }
            TryClose();
        }

        public bool CanAdd
        {
            get
            {
                if (BorrowsList.Count < numberOfAvaibleBooks && SelectedUser != null)
                    return true;
                else
                    return false;
            }
        }

        public void Cancel()
        {
            TryClose();
        }
              
    }
}
