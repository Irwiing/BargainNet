﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Entities
{
    public class Chat : EntityBase
    {
        public Chat()
        {
            CreateDate = DateTime.Now;
            Status = Status.Active;
        }
        public virtual AdAuction Auction { get; set; }
        public virtual User AuctionOwner { get; set; }
        public virtual User AuctionWinner { get; set; }
        public Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public List<ChatMessage> Messages { get; set; }

    }
}
