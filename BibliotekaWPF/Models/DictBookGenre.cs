using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.Models
{
    [Table("DictBookGenres")]
    public class DictBookGenre
    {
        [Key]
        public int BookGenreId { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey("DictBookGenre")]
        public ICollection<Book> Book { get; set; }

    }
}
