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

        public async Task<List<AdAuction>> FindAllAsync()
        {
            return await _dataContext.Auctions.Include(a => a.AdAcutionSettings).Include(a => a.Category).Include(a => a.Offers).ThenInclude(offer => offer.User).ToListAsync();
        }

        public async Task<AdAuction> GetByIdAsync(Guid objId)
        {
            return await _dataContext.Auctions.Include(a => a.AdAcutionSettings).Include(a => a.Category).Include(a => a.Offers).Include("Offers.User").FirstOrDefaultAsync(a => a.Id == objId) ;

        }

        public async Task UpdateAsync(AdAuction obj)
        {
            var saved = false;
            while (!saved)
            {
                try
                {
                    // Attempt to save changes to the database
                    _dataContext.Auctions.Update(obj);
                    await _dataContext.SaveChangesAsync();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is Offer)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];

                                // TODO: decide which value should be written to database
                                // proposedValues[property] = <value to be saved>;
                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
            }
           
        }
    }
}
