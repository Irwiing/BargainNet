using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Entities
{
    public class ChatMessage : EntityBase
    {
        public ChatMessage()
        {
            SendDate = DateTime.Now;

        }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
        public User Sender { get; set; }

    }
}
