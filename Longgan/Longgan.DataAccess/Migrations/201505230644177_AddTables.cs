namespace Longgan.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(maxLength: 100),
                        Title = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false, maxLength: 3000),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.New",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        Title = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false, maxLength: 3000),
                        Hot = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        Title = c.String(nullable: false, maxLength: 100),
                        Content = c.String(maxLength: 3000),
                        PicName = c.String(maxLength: 100),
                        IntroName = c.String(maxLength: 100),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SetCase",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        Title = c.String(maxLength: 100),
                        Content = c.String(maxLength: 3000),
                        PicName = c.String(maxLength: 100),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SetCase");
            DropTable("dbo.Product");
            DropTable("dbo.New");
            DropTable("dbo.Message");
        }
    }
}
