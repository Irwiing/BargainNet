using BargainNet.Core.Contracts.Entities;
using BargainNet.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Contracts.Services
{
    public interface IUserProfileService
    {
        Task CreateProfile(UserProfile userProfile, string userId, int personType, IFormFile profilePic, IFormFile documentPic);
        Task<User> GetProfile(string id);
        Task<List<Category>> GetCategories();
        Task<bool> HasSlots(string userName);
        Task SetInterests(string userId, Guid[] interests);
        Task<List<Category>> GetInterests(string userId);
    }
}
