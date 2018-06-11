using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.Models
{
    [Table("Borrows")]
    public class Borrow
    {
        [Key]
        public int BorrowId { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public User User { get; set; }

        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }
        
        public Book Book { get; set; }

        [Required]
        public DateTime? FromDate { get; set; }
        [Required]
        public DateTime? ToDate { get; set; }
        [Required]
        public Boolean IsReturned { get; set; } = false;

        
        

    }
}
