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
    }
}
