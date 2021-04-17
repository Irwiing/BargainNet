using BargainNet.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User obj);
        Task<IdentityResult> UpdateAsync(User obj);
        Task DeleteAsync(string objId);
        Task<User> GetByIdAsync(string objId);
        Task<User> GetByUserNameAsync(string objUserName);
        Task<List<User>> FindAllAsync();
    }
}
