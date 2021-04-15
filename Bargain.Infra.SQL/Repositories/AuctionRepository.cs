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
    public class AuctionRepository : IAuctionRepository
    {
        private readonly DataContext _dataContext;
        public AuctionRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task AddAsync(AdAuction obj)
        {
            await _dataContext.Auctions.AddAsync(obj);
            await _dataContext.SaveChangesAsync();
        }

        public Task DeleteAsync(int objId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AdAuction>> FindAllAssync()
        {
            return await _dataContext.Auctions.Include(a => a.AdAcutionSettings).Include(a => a.Category).Include(a => a.Offers).ToListAsync();
        }

        public async Task<AdAuction> GetByIdAsync(Guid objId)
        {
            return await _dataContext.Auctions.Include(a => a.AdAcutionSettings).Include(a => a.Category).Include(a => a.Offers).FirstOrDefaultAsync(a => a.Id == objId) ;

        }

        public async Task UpdateAsync(AdAuction obj)
        {
            _dataContext.Auctions.Update(obj);
            await _dataContext.SaveChangesAsync();
        }
    }
}
