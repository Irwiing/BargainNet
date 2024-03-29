﻿using BargainNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Contracts.Services
{
    public interface IUserService
    {
        Task<bool> HasProfile(string userName);
        Task<bool> IsLegalPerson(string userName);
        Task<User> GetUserByName(string userName);
        Task<User> GetUser(string userId);
        Task<bool> UpdateUser(User user);
    }
}
