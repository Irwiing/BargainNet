using BargainNet.Core.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Contracts.Repositories
{
    public interface IRepositoryBase<T> where T : IEntityBase
    {
        Task AddAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(int objId);
        Task<T> GetByIdAsync(Guid objId);
        Task<List<T>> FindAllAssync();
    }
}
