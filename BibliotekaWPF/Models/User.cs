using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotekaWPF.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [NotMapped]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }
        [Required(ErrorMessage =("First name is required"))]
        public string FirstName { get; set; }
        [Required(ErrorMessage = ("Last name is required"))]
        public string LastName { get; set; }
        [Required(ErrorMessage =("Birth date is required"))]
        [Range(typeof(DateTime), "01/01/1753", "31/12/9999", ErrorMessage = ("Birth date is out of range"))]
        public DateTime? BirthTime { get; set; }
        [Required(ErrorMessage = ("E-mail is required"))]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public DateTime AddDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = false;


        [ForeignKey("User")]
        public ICollection<Borrow> Borrows { get; set; }

        

        
        

    }
}
