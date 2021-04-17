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
    public class ChatRepository : IChatRepository
    {
        private readonly DataContext _dataContext;
        public ChatRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task AddAsync(Chat obj)
        {
            await _dataContext.Chats.AddAsync(obj);
            await _dataContext.SaveChangesAsync();
        }

        public Task DeleteAsync(int objId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Chat>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Chat> GetByIdAsync(Guid objId)
        {
            return await _dataContext.Chats.Include(chat => chat.Messages).Include("Messages.Sender").FirstOrDefaultAsync(chat => chat.Id == objId);

        }

        public async Task UpdateAsync(Chat obj)
        {
            _dataContext.Chats.Update(obj);
            await _dataContext.SaveChangesAsync();
        }
    }
}
