using Longgan.DataAccess.Migrations;
using Longgan.Models.Home;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("LGContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;

            //if (!Database.Exists())
            //{
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>());
            //}
            //else
            //{
            //    InitializeDatabaseForEnvironment();
            //}
        }

        internal DbSet<New> News { get; set; }
        internal DbSet<Product> Products { get; set; }
        internal DbSet<SetCase> SetCases { get; set; }
        internal DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void Seed(DatabaseContext context)
        {
        }
    }

    public class CreateDatabaseInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            context.Seed(context);
            base.Seed(context);
        }
    }

    public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            context.Seed(context);
            base.Seed(context);
        }
    }
}
