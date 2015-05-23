using Longgan.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.DataAccess.Home
{
    public class NewsDA : GenericRepository<New>
    {
        public List<New> GetNews()
        {
            return base.Get().OrderByDescending(p => p.Created).ToList();
        }

        public New GetNew(string Id)
        {
            return base.Get(p => p.Id == Id).FirstOrDefault();
        }

        public void UpdateNews(New n)
        {
            base.Update(n);
        }

        public void AddNews(New n)
        {
            base.Insert(n);
        }

        public void RemoveNews(New n)
        {
            base.Delete(n);
        }
    }
}
