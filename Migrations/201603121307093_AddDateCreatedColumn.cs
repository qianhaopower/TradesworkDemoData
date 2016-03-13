namespace CentralDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateCreatedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "CreatedDate");
        }
    }
}
