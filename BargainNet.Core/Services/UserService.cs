using BargainNet.Core.Contracts.Repositories;
using BargainNet.Core.Contracts.Services;
using BargainNet.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUser(string userId)
        {
           return await _userRepository.GetByIdAsync(userId);
        }
        public async Task<User> GetUserByName(string userName)
        {
            return await _userRepository.GetByUserNameAsync(userName);
        }
        public async Task<bool> HasProfile(string userName)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);

            if (user.NaturalPerson == null && user.LegalPerson == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> UpdateUser(User user)
        {
            var result = await _userRepository.UpdateAsync(user);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
