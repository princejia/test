namespace Longgan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMessage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Message", "Phone", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Message", "Phone", c => c.String(maxLength: 100));
        }
    }
}
