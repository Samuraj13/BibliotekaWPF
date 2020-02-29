using BibliotekaWPF.Data;
using BibliotekaWPF.ModelsDTO;
using Caliburn.Micro;
using IronPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.ViewModels
{
    public class ReportUserViewModel : Screen
    {
        public List<BorrowDetailsDTO> NumberOfBorrowsPerUser { get; set; }
        public ReportUserViewModel()
        {
            NumberOfBorrowsPerUser = BorrowServices.NumberOfBorrowsPerUser(SearchLastName);
            foreach (var item in NumberOfBorrowsPerUser)
            {
                item.UserFullName = item.UserFirstName + " " + item.UserLastName;
            }
        }
        private string searchLastName;

        public string SearchLastName
        {
            get { return searchLastName; }
            set { searchLastName = value;
                NumberOfBorrowsPerUser = BorrowServices.NumberOfBorrowsPerUser(SearchLastName);
                NotifyOfPropertyChange(() => NumberOfBorrowsPerUser);
            }
        }

        public void CreateReport()
        {
            var parent = (ReportsViewModel)this.Parent;
            var reportBooks = parent.Items.OfType<ReportBookViewModel>().FirstOrDefault();
            var list = reportBooks.NumberOfBorrowsPerTitle;

            StringBuilder htmlToPdfString = new StringBuilder();
            if (list.Count > 0)
            {
                htmlToPdfString.Append("<h1>Books report</h1>");
                htmlToPdfString.Append("</br>"); 
                htmlToPdfString.Append("<table border=\"1\" style=\"width:100%\">");
                htmlToPdfString.Append("<tr><th>Title</th><th>Author</th><th>Genre</th><th>Number of borrows</th></tr>");
                foreach ( var item in list)
                {
                    htmlToPdfString.Append($"<tr><th>{item.BookTitle}</th><th>{item.BookAuthor}</th><th>{item.BookGenre}</th><th>{item.BorrowsCount}</th></tr>");
                }
                htmlToPdfString.Append("</table>");
            }
            if (NumberOfBorrowsPerUser.Count > 0)
            {
                htmlToPdfString.Append("</br>");
                htmlToPdfString.Append("</br>");
                htmlToPdfString.Append("</br>");
                htmlToPdfString.Append("<h1>Users report</h1>");
                htmlToPdfString.Append("</br>");
                htmlToPdfString.Append("<table border=\"1\" style=\"width:100%\">");
                htmlToPdfString.Append("<tr><th>User full name</th><th>Number of borrows</th></tr>");
                foreach (var item in NumberOfBorrowsPerUser)
                {
                    htmlToPdfString.Append($"<tr><th>{item.UserFullName}</th><th>{item.BorrowsCount}</th></tr>");
                }
                htmlToPdfString.Append("</table>");
            }

            HtmlToPdf myHtmlToPdf = new IronPdf.HtmlToPdf();

            string path = string.Format("{0}\\{1}_Report.pdf", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_fff"));
            myHtmlToPdf.RenderHtmlAsPdf(htmlToPdfString.ToString()).SaveAs(path);

        }
    }
}
