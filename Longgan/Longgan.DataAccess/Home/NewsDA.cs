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
    }
}
