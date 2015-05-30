using Longgan.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.DataAccess.Home
{
    public class MessageDA : GenericRepository<Message>
    {
        public List<Message> GetMessages()
        {
            return base.Get().OrderByDescending(p => p.Created).ToList();
        }

        public Message GetMessage(string Id)
        {
            return base.Get(p => p.Id == Id).FirstOrDefault();
        }

        public void AddMessage(Message msg)
        {
            base.Insert(msg);
        }

        public void UpdateMessage(Message n)
        {
            base.Update(n);
        }

        public void RemoveMessage(Message n)
        {
            base.Delete(n);
        }
    }
}
