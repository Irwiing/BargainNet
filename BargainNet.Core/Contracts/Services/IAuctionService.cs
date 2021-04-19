using BargainNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Contracts.Services
{
    public interface IAuctionService
    {
        Task<List<AdAuction>> GetUserInterestAuctions(string userId);
        Task<List<AdAuction>> GetAuctions();
        Task<AdAuction> GetAuction(Guid auctionId);
        Task CreateAuction(string userId, AdAuction auction);
        Task CreateOffer(Offer offer, Guid idAuction, string idUser);
        Task EndAuction(Guid idAuction, string id);
        User GetWinner(AdAuction auction);
        Task AddSlot(string userId);
    }
}
