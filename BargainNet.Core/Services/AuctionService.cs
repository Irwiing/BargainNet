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

            return await _auctionRepository.FindAllAsync();
        }
        public async Task<AdAuction> GetAuction(Guid auctionId)
        {

            return await _auctionRepository.GetByIdAsync(auctionId);
        }
        public async Task<List<AdAuction>> GetUserInterestAuctions(string userId)
        {
            List<AdAuction> interestAuction = new List<AdAuction>();
            var allAuctions = await _auctionRepository.FindAllAsync();
            var user = await _userService.GetUser(userId);
            return allAuctions.FindAll(a => user.UserProfile.Interests.Contains(a.Category) && a.AdAcutionSettings.Status == Status.Active && !user.UserProfile.AdAuctions.Contains(a));
        }

        public async Task CreateOffer(Offer offer, Guid idAuction, string idUser)
        {
            var auction = await _auctionRepository.GetByIdAsync(idAuction);
            offer.User = await _userService.GetUser(idUser);
            var betterOffer = auction.Offers.FirstOrDefault(o => o.Value == auction.Offers.Max(o => o.Value));
            if (offer.Value <= auction.Value || offer.Value <= betterOffer?.Value )
            {
                throw new Exception("Seu lance deve ser maior que o ultimo!");
            }
            if(betterOffer?.User.Id == offer.User.Id)
            {
                throw new Exception("Você não pode dar dois lances seguidos");
            }
            auction.Offers.Add(offer);
            await _auctionRepository.UpdateAsync(auction);
        }
        public async Task EndAuction(Guid idAuction, string idUser)
        {
            var user = await _userService.GetUser(idUser);
            var auction = await _auctionRepository.GetByIdAsync(idAuction);
            auction.AdAcutionSettings.Status = Status.Inactive;

            await _userService.UpdateUser(user);
        }
        public User GetWinner(AdAuction auction)
        {
            if (auction.Offers.Any())
            {
                var winnerOffer = auction.Offers.FirstOrDefault(offer => offer.Value == auction.Offers.Max(o => o.Value));
                return winnerOffer.User;
            }
            return null;
        }
        public async Task AddSlot(string userId)
        {
            var user = await _userService.GetUser(userId);
            PaydPackage newPackage = new PaydPackage()
            {
                Quantity = 3
            };
            user.UserProfile.PaydPackages.Add(newPackage);
            user.UserProfile.TotalSlotsAd += newPackage.Quantity;
            await _userService.UpdateUser(user);
        }
    }
}
