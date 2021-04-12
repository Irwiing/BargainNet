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

        public async Task<bool> CreateProfile(NaturalPerson userProfile, string userName)
        {
            var user = await _userService.GetUserByName(userName);

            user.NaturalPerson = userProfile;
            if (await _userService.UpdateUser(user))
            {
                return true;
            }
            return false;
        }
        public async Task<bool> CreateProfile(LegalPerson userProfile, string userName)
        {
            var user = await _userService.GetUserByName(userName);

            user.LegalPerson = userProfile;
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
            return await _userService.GetUserByName(userName);
        }
    }
}
