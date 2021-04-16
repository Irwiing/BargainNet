﻿using BargainNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Contracts.Services
{
    public interface IChatService
    {
        Task CreateChat(string ownerId, AdAuction auction);
    }
}
