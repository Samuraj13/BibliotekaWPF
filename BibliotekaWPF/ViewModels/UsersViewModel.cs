using BibliotekaWPF.Data;
using BibliotekaWPF.Models;
using BibliotekaWPF.ModelsDTO;
using BibliotekaWPF.Views;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;


namespace BibliotekaWPF.ViewModels
{
    public class UsersViewModel :Screen
    {
        public List<UserDTO> Users { get; set; }
        public UsersViewModel()
        {
            
            Reload();
        }

 

        
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            
        }

        public void LoadAddUserPage()
        {
            IWindowManager manager = new WindowManager();
            AddUserViewModel add = new AddUserViewModel();
            manager.ShowDialog(add, null, null);
            Reload();

        }

        public void LoadDeletePage(UserDTO user)
        {
            
            IWindowManager manager = new WindowManager();
            DeleteViewModel delete = new DeleteViewModel(user.UserId);
            manager.ShowDialog(delete, null, null);
            Reload();
        }

        public void LoadModifyUserPage(UserDTO user)
        {
            IWindowManager manager = new WindowManager();
            ModifyUserViewModel modify = new ModifyUserViewModel(user);
            manager.ShowDialog(modify, null, null);
            Reload();
        }

        public void LoadDetailsPage(UserDTO user)
        {

            IWindowManager manager = new WindowManager();
            UserDetailsViewModel details = new UserDetailsViewModel(user);
            manager.ShowDialog(details, null, null);
            Reload();
        }

        public void Reload()
        {
            Users = UserServices.GetAll();
            NotifyOfPropertyChange(() => Users);
        }



    }
}
