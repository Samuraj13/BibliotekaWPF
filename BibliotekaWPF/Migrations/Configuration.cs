namespace BibliotekaWPF.Migrations
{
    using BibliotekaWPF.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BibliotekaWPF.Data.LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;            
        }

        protected override void Seed(BibliotekaWPF.Data.LibraryContext db)
        {
            
        }
    }
}
