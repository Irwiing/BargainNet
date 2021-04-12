using BargainNet.Core.Contracts.Repositories;
using BargainNet.Core.Entities;
using BargainNet.Infra.SQL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Infra.SQL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dataContext;
        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task AddAsync(Category obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int objId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> FindAllAssync()
        {
            var a = await _dataContext.Categories.ToListAsync();
          
            return (a);
        }

        public async Task<Category> GetByIdAsync(string objId)
        {
            return (await _dataContext.Categories.FirstOrDefaultAsync(category => category.Id == Guid.Parse(objId)));
        }

        public Task UpdateAsync(Category obj)
        {
            throw new NotImplementedException();
        }
    }
}
