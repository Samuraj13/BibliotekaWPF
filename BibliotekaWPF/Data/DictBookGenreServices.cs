using BibliotekaWPF.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.Data
{
    public class DictBookGenreServices
    {
        public static BindableCollection<DictBookGenre> GetAll()
        {
            using (LibraryContext db = new LibraryContext())
            {
                var result = new BindableCollection<DictBookGenre>(db.DictBookGenres.ToList());
                return result;
            }
        }
    }
}
