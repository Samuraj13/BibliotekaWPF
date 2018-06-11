using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaWPF.ViewModels
{
    public class ReportsViewModel : Conductor<Screen>.Collection.AllActive
    {
        public ReportsViewModel()
        {
            Items.Add(ReportBookPage);
            Items.Add(ReportUserPage);
        }

        public Screen ReportBookPage { get; set; } = new ReportBookViewModel();
        public Screen ReportUserPage { get; set; } = new ReportUserViewModel();
    }
}
