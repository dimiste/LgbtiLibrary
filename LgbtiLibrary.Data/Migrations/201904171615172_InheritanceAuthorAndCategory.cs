namespace LgbtiLibrary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InheritanceAuthorAndCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        UrlBook = c.String(nullable: false),
                        UrlImage = c.String(),
                        Author_Id = c.Guid(nullable: false),
                        Category_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.BookElements", t => t.Author_Id, cascadeDelete: false)
                .ForeignKey("dbo.BookElements", t => t.Category_Id, cascadeDelete: false)
                .Index(t => t.Author_Id)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Category_Id", "dbo.BookElements");
            DropForeignKey("dbo.Books", "Author_Id", "dbo.BookElements");
            DropIndex("dbo.Books", new[] { "Category_Id" });
            DropIndex("dbo.Books", new[] { "Author_Id" });
            DropTable("dbo.Books");
            DropTable("dbo.BookElements");
        }
    }
}
