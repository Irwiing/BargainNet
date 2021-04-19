using BargainNet.Core.Contracts.Entities;
using BargainNet.Core.Contracts.Repositories;
using BargainNet.Core.Contracts.Services;
using BargainNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserService _userService;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ICategoryService _categoryService;

        public UserProfileService(IUserService userService, 
            IUserProfileRepository userProfileRepository,
            ICategoryService categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<bool> CreateProfile(UserProfile userProfile, string userName)
        {
            var user = await GetProfile(userName);

            user.UserProfile = userProfile;
            if (await _userService.UpdateUser(user))
            {
                return true;
            }
            return false;
        }

        public Task<List<Category>> GetCategories()
        {
            return _categoryService.GetCategories();
        }

        public async Task<User> GetProfile(string userName)
        {
            return await _userService.GetUser(userName);
        }

        public async Task<bool> HasSlots(string userName)
        {
            var user = await GetProfile(userName);

            if (user.UserProfile.AdAuctions.Count < user.UserProfile.TotalSlotsAd)
            {
                return true;
            }
            return false;
        }
        public async Task SetInterests(string userId, Guid[] interests)
        {
            var user = await _userService.GetUser(userId);
            List<Category> interestCategories = new List<Category>();
            foreach (var interest in interests)
            {
                interestCategories.Add(await _categoryService.GetCategory(interest));
            }
            user.UserProfile.Interests = interestCategories;
            await _userService.UpdateUser(user);
        }
    }
}
