using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required(ErrorMessage = ("Author is required"))]
        public string Author { get; set; }
        [Required(ErrorMessage = ("Title is required"))]
        public string Title { get; set; }
        [Range(typeof(DateTime), "01/01/1753", "31/12/9999", ErrorMessage =("Release date is out of range"))]
        public DateTime? ReleaseDate { get; set; }
        [Required(ErrorMessage = ("ISBN is required"))]
        [Range(0, 9999999999999, ErrorMessage = ("ISBN must be a number"))]
        public string ISBN { get; set; }
        [ForeignKey("DictBookGenre")]
        [Required]
        public int BookGenreId { get; set; }

        [Required(ErrorMessage = ("Count is required"))]
        [Range(0,int.MaxValue,ErrorMessage =("Count must be greater than 0"))]
        public int Count { get; set; }
        [Required]
        public DateTime? AddDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        public DictBookGenre DictBookGenre { get; set; }
        [ForeignKey("Book")]
        public ICollection<Borrow> Borrow { get; set; }

    }
}
