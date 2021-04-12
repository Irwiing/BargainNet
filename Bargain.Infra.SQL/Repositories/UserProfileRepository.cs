﻿using BargainNet.Core.Contracts.Repositories;
using BargainNet.Core.Entities;
using BargainNet.Infra.SQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Infra.SQL.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly DataContext _dataContext;
        public UserProfileRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task AddAsync(NaturalPerson obj)
        {
            await _dataContext.NaturalPeople.AddAsync(obj);
            await _dataContext.SaveChangesAsync();
        }

        public Task DeleteAsync(int objId)
        {
            throw new NotImplementedException();
        }

        public Task<List<NaturalPerson>> FindAllAssync()
        {
            throw new NotImplementedException();
        }

        public Task<NaturalPerson> GetByIdAsync(string objId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(NaturalPerson obj)
        {
            throw new NotImplementedException();
        }
    }
}
