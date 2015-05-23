using Longgan.Models.Home;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DatabaseContext context)
        {
            //var news = new List<New>{

            //    new Longgan.Models.Home.New{Id=Guid.NewGuid().ToString(),Title ="ddddd",Content ="daaaaaaaaaaaaaaa",Created=DateTime.Now}
            //};

            //news.ForEach(c => context.News.Add(c));
            //context.SaveChanges();
        }
    }
}
