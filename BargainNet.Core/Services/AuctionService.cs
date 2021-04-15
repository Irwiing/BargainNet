using BargainNet.Core.Contracts.Repositories;
using BargainNet.Core.Contracts.Services;
using BargainNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IUserService _userService;
        public AuctionService(IAuctionRepository auctionRepository, IUserService userService)
        {
            _auctionRepository = auctionRepository;
            _userService = userService;
        }
        public async Task CreateAuction(string userId, AdAuction auction)
        {
            var user = await _userService.GetUser(userId);
            user.UserProfile.AdAuctions.Add(auction);
            await _userService.UpdateUser(user);
            
        }
        public async Task<List<AdAuction>> GetAuctions()
        {
           
            return  await _auctionRepository.FindAllAssync();
        }
        public async Task<AdAuction> GetAuction(Guid auctionId)
        {

            return await _auctionRepository.GetByIdAsync(auctionId);
        }
        public async Task<List<AdAuction>> GetUserInterestAuctions(string userId)
        {
            List<AdAuction> interestAuction = new List<AdAuction>();
            var allAuctions = await _auctionRepository.FindAllAssync();
            var user = await _userService.GetUser(userId);
            return allAuctions.FindAll(a => user.UserProfile.Interests.Contains(a.Category));
        }

        public async Task CreateOffer(Offer offer, Guid idAuction)
        {
            var auction = await _auctionRepository.GetByIdAsync(idAuction);
            auction.Offers.Add(offer);
            await _auctionRepository.UpdateAsync(auction);
        }
    }
    
}
