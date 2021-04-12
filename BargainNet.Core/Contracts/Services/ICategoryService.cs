using BargainNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Contracts.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(string id);
    }
}
