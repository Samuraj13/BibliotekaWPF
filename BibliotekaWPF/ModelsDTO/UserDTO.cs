using BibliotekaWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.ModelsDTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthTime { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }
        
        public ICollection<Borrow> Borrows { get; set; }
        


        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

        public int ActualBorrowsCount { get; set; }
        
        public string Active
        {
            get
            {
                if (this.IsActive == true)
                    return "Yes";
                else
                    return "No";
            }
        }

        public bool CanDelete
        {
            get
            {
                return ActualBorrowsCount == 0;
            }

        }
    }
}
