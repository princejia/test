using Longgan.DataAccess.Home;
using Longgan.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.Logics.Home
{
    public class NewsLogic : Logic
    {
        NewsDA da = new NewsDA();
        public List<New> GetNews()
        {
            return da.GetNews();
        }

        public New GetNew(string Id)
        {
            return da.GetNew(Id);
        }

        public void UpdateNews(New n)
        {            
            da.UpdateNews(n);
        }

        public void AddNews(New n)
        {
            n.Id = Guid.NewGuid().ToString();
            n.Created = DateTime.Now;
            da.AddNews(n);
        }

        public void RemoveNews(New n)
        {
            da.RemoveNews(n);
        }
    }
}
