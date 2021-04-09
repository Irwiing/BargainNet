using BargainNet.Core.Contracts.Entities;
using BargainNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Contracts.Services
{
    public interface IUserProfileService
    {
        Task<bool> CreateProfile(NaturalPerson userProfile, string userName);
        Task<User> GetProfile(string id);
    }
}
