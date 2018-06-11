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
    public class DeleteViewModel : Screen
    {
        int _id;
        public DeleteViewModel(int id)
        {
            _id = id;
        }
        
        public void DeleteAccept()
        {
            UserServices.Delete(_id);
            TryClose();

        }

        public void DeleteDiscard()
        {
            TryClose();
        }
    }
}
