namespace CentralDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJsonColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "JsonContent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "JsonContent");
        }
    }
}
