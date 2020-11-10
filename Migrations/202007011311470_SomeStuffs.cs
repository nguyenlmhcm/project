namespace DoAn1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeStuffs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "IsShiped", c => c.Boolean(nullable: false));
            AddColumn("dbo.Coupons", "Code", c => c.String());
            DropColumn("dbo.Carts", "Address");
            DropColumn("dbo.Carts", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "Phone", c => c.String());
            AddColumn("dbo.Carts", "Address", c => c.String());
            DropColumn("dbo.Coupons", "Code");
            DropColumn("dbo.Carts", "IsShiped");
        }
    }
}
