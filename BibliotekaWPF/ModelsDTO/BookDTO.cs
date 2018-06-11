using BibliotekaWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.ModelsDTO
{
    public class BookDTO
    {
       
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ISBN { get; set; }
        public int BookGenreId { get; set; }
        public int Count { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public DictBookGenre DictBookGenre { get; set; }
        public ICollection<Borrow> Borrow { get; set; }


        public string BookGenreName { get; set; }
    }
}
