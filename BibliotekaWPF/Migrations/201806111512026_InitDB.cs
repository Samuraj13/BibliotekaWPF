namespace BibliotekaWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Author = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        ReleaseDate = c.DateTime(),
                        ISBN = c.String(nullable: false),
                        BookGenreId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        AddDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.DictBookGenres", t => t.BookGenreId, cascadeDelete: true)
                .Index(t => t.BookGenreId);
            
            CreateTable(
                "dbo.Borrows",
                c => new
                    {
                        BorrowId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        IsReturned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BorrowId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        BirthTime = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(),
                        AddDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.DictBookGenres",
                c => new
                    {
                        BookGenreId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BookGenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "BookGenreId", "dbo.DictBookGenres");
            DropForeignKey("dbo.Borrows", "UserId", "dbo.Users");
            DropForeignKey("dbo.Borrows", "BookId", "dbo.Books");
            DropIndex("dbo.Borrows", new[] { "BookId" });
            DropIndex("dbo.Borrows", new[] { "UserId" });
            DropIndex("dbo.Books", new[] { "BookGenreId" });
            DropTable("dbo.DictBookGenres");
            DropTable("dbo.Users");
            DropTable("dbo.Borrows");
            DropTable("dbo.Books");
        }
    }
}
