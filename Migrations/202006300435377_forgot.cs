namespace DoAn1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forgot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewReleases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewReleases", "BookId", "dbo.Books");
            DropIndex("dbo.NewReleases", new[] { "BookId" });
            DropTable("dbo.NewReleases");
        }
    }
}
