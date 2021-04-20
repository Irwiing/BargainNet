using BargainNet.Core.Contracts.Entities;
using BargainNet.Core.Contracts.Repositories;
using BargainNet.Core.Contracts.Services;
using BargainNet.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserService _userService;

        private readonly ICategoryService _categoryService;
        private readonly IImageService _imageService;

        public UserProfileService(IUserService userService,
            IImageService imageService,
            ICategoryService categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _imageService = imageService;
        }

        public async Task CreateProfile(UserProfile userProfile, string userId, int personType, IFormFile profilePic, IFormFile documentPic)
        {
            bool isLegalPerson = personType == 0 ? false : true;
            if (profilePic == null)
            {
                throw new Exception("Insira uma foto!");
            }
            userProfile.ProfilePic = _imageService.ConvertImgToString(profilePic);
            if (isLegalPerson)
            {
                if (!Maoli.Cnpj.Validate(userProfile.Document)) throw new Exception("CNPJ Inválido!");
                if (documentPic == null) throw new Exception("Insira o documento de comprovação!");
                userProfile.LegalPerson.DocumentPic = _imageService.ConvertImgToString(documentPic);
                userProfile.NaturalPerson = null;
                userProfile.TotalSlotsAd = Constants.LegalPersonFreeSlots;
            }
            else
            {
                if (!Maoli.Cpf.Validate(userProfile.Document)) throw new Exception("Cpf Inválido!");
                userProfile.LegalPerson = null;
                userProfile.TotalSlotsAd = Constants.NaturalPersonFreeSlots;
            }
            var user = await GetProfile(userId);

            user.UserProfile = userProfile;
            await _userService.UpdateUser(user);
        }

        public Task<List<Category>> GetCategories()
        {
            return _categoryService.GetCategories();
        }

        public async Task<User> GetProfile(string userName)
        {
            return await _userService.GetUser(userName);
        }

        public async Task<bool> HasSlots(string userName)
        {
            var user = await GetProfile(userName);

            if (user.UserProfile.AdAuctions.Count < user.UserProfile.TotalSlotsAd)
            {
                return true;
            }
            return false;
        }
        public async Task SetInterests(string userId, Guid[] interests)
        {
            var user = await _userService.GetUser(userId);
            List<Category> interestCategories = new List<Category>();
            foreach (var interest in interests)
            {
                interestCategories.Add(await _categoryService.GetCategory(interest));
            }
            user.UserProfile.Interests = interestCategories;
            await _userService.UpdateUser(user);
        }
        public async Task<List<Category>> GetInterests(string userId)
        {
            var categories = await _categoryService.GetCategories();
            var user = await _userService.GetUser(userId);

            categories.ForEach(c => {
                if (user.UserProfile.Interests.Contains(c)) {
                    c.isChecked = true;
                }
                else
                {
                    c.isChecked = false;
                }
            });

            return categories;
        }
    }
}
