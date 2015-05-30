namespace Longgan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Type", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Type");
        }
    }
}
