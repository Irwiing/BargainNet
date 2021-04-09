using BargainNet.Core.Contracts.Repositories;
using BargainNet.Core.Entities;
using BargainNet.Infra.SQL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bargain.Infra.SQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;
        public UserRepository(DataContext dataContext, UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }
        public Task AddAsync(User obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string objId)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> FindAllAssync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(string userId)
        {
          return await _dataContext.UserPerson.Include(up => up.NaturalPerson).FirstOrDefaultAsync(u => u.Id == userId);
            
        }

        public async Task<User> GetByUserNameAsync(string objUserName)
        {
            return await _userManager.FindByNameAsync(objUserName);
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
