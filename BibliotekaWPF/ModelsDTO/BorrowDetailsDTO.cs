using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.ModelsDTO
{
    public class BorrowDetailsDTO
    {
        public int BorrowId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string UserFullName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public bool IsSelected { get; set; }
        public bool IsReturned { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int BorrowsCount { get; set; }
        public string BookGenre { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        
    }
}
