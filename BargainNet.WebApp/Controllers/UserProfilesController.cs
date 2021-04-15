using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BargainNet.Core.Entities;
using BargainNet.Infra.SQL.Data;
using BargainNet.Core.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BargainNet.WebApp.ModelView;
using Microsoft.AspNetCore.Http;

namespace BargainNet.WebApp.Controllers
{
    public class UserProfilesController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly ICategoryService _categoryService;
        private readonly IImageService _imageService;
        private readonly IAuctionService _auctionService;
        public UserProfilesController(IUserProfileService userProfileService, 
            ICategoryService categoryService,
            IImageService imageService,
            IAuctionService auctionService)
        {
            _categoryService = categoryService;
            _userProfileService = userProfileService;
            _imageService = imageService;
            _auctionService = auctionService;
        }

        [Authorize]
        public async Task<IActionResult> Details()
        {
            var userName = User.Identity.Name;
            var auctions = await _auctionService.GetUserInterestAuctions(userName);
            ViewData["myAuctions"] = auctions;
            return View(await _userProfileService.GetProfile(userName));
        }

        // GET: UserProfiles/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetCategories();
            ViewData["Categories"] = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(UserProfile createProfile, Guid[] interests, int personType, IFormFile profilePic)
        {
            bool isLegalPerson = personType == 0 ? false : true;
            if (ModelState.IsValid)
            {
                createProfile.ProfilePic = _imageService.ConvertImgToString(profilePic);
                if (isLegalPerson)
                {
                    createProfile.NaturalPerson = null;
                    createProfile.TotalSlotsAd = Constants.LegalPersonFreeSlots;
                }else
                {
                    createProfile.LegalPerson = null;
                    createProfile.TotalSlotsAd = Constants.NaturalPersonFreeSlots;
                }
                bool created = false;
                var userName = User.Identity;
                List<Category> interestCategories = new List<Category>();
                foreach (var interest in interests)
                {
                    interestCategories.Add(await _categoryService.GetCategory(interest));
                }
                createProfile.Interests = interestCategories;
                    created = await _userProfileService.CreateProfile(createProfile, userName.Name);

                if(created) return RedirectToAction(nameof(Details), new { userName = userName.Name });

            }
            return View(createProfile);
        }
    }
}
