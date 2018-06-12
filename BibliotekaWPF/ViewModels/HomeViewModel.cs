using BibliotekaWPF.Data;
using BibliotekaWPF.Models;
using BibliotekaWPF.ModelsDTO;
using BibliotekaWPF.ViewModels;
using Caliburn.Micro;
using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BibliotekaWPF.ViewModels
{
    public class HomeViewModel : Conductor<object>
    {
        public bool isDark { get; set; } = true;
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
        public void LoadStylePage()
        {
            ActiveItem = new StyleViewModel();
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
        public void Changepalette(bool isDark)
        {
            var existingResourceDictionary = Application.Current.Resources.MergedDictionaries
                  .Where(rd => rd.Source != null)
                  .SingleOrDefault(rd => Regex.Match(rd.Source.OriginalString, @"(\/MaterialDesignThemes.Wpf;component\/Themes\/MaterialDesignTheme\.)((Light)|(Dark))").Success);
            if (existingResourceDictionary == null)
                throw new ApplicationException("Unable to find Light/Dark base theme in Application resources.");

            var source =
                $"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{(isDark ? "Dark" : "Light")}.xaml";
            var newResourceDictionary = new ResourceDictionary() { Source = new Uri(source) };

            Application.Current.Resources.MergedDictionaries.Remove(existingResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newResourceDictionary);

            var existingMahAppsResourceDictionary = Application.Current.Resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.OriginalString, @"(\/MahApps.Metro;component\/Styles\/Accents\/)((BaseLight)|(BaseDark))").Success);
            if (existingMahAppsResourceDictionary == null) return;

            source =
                $"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(isDark ? "BaseDark" : "BaseLight")}.xaml";
            var newMahAppsResourceDictionary = new ResourceDictionary { Source = new Uri(source) };

            Application.Current.Resources.MergedDictionaries.Remove(existingMahAppsResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newMahAppsResourceDictionary);
        }
        public void ChangeToLightTheme()
        {
            Changepalette(false);
        }
        public void ChangeToDarkTheme()
        {
            Changepalette(true);
        }
    }
}
