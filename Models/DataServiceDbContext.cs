using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Entity;

namespace DataService.Models
{
    public class DataServiceDbContext : DbContext
    {
        public DataServiceDbContext() : base("name=AzureContext")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DataServiceDbContext>());
            //Database.Initialize(false);

        }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<PropertyComponent> PropertyComponents { get; set; }
        public DbSet<ComponentItem> ComponentItems { get; set; }

    }
    public class MyInitializer : DropCreateDatabaseAlways<DataServiceDbContext>
    {
        protected override void Seed(DataServiceDbContext context)
        {
            // seed database here
        }
    }

}
