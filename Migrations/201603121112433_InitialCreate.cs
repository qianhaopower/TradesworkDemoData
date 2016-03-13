namespace CentralDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reports", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PropertyComponents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.ComponentItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        Comment = c.String(),
                        MarketTag = c.Boolean(nullable: false),
                        PropertyComponentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PropertyComponents", t => t.PropertyComponentId, cascadeDelete: true)
                .Index(t => t.PropertyComponentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComponentItems", "PropertyComponentId", "dbo.PropertyComponents");
            DropForeignKey("dbo.PropertyComponents", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "Id", "dbo.Reports");
            DropIndex("dbo.ComponentItems", new[] { "PropertyComponentId" });
            DropIndex("dbo.PropertyComponents", new[] { "ClientId" });
            DropIndex("dbo.Clients", new[] { "Id" });
            DropTable("dbo.ComponentItems");
            DropTable("dbo.PropertyComponents");
            DropTable("dbo.Reports");
            DropTable("dbo.Clients");
        }
    }
}
