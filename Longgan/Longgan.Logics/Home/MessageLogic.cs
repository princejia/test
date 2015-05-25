using Longgan.DataAccess.Home;
using Longgan.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.Logics.Home
{
    public class MessageLogic : Logic
    {
        MessageDA da = new MessageDA();

        public List<Message> GetMessages()
        {
            return da.GetMessages();
        }

        public void AddMessage(Message msg)
        {
            msg.Id = Guid.NewGuid().ToString();
            msg.Created = DateTime.Now;
            da.AddMessage(msg);
        }
    }
}
