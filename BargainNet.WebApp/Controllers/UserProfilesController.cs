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
    [Authorize]
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


        public async Task<IActionResult> Details()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userProfileService.GetProfile(userId);
            if (!user.UserProfile.Interests.Any())
            {
                return RedirectToAction("SetInterests");
            }

            if (user.UserProfile == null)
            {
                return RedirectToAction("Create");
            }
            var auctions = await _auctionService.GetUserInterestAuctions(userId);
            ViewData["myAuctions"] = auctions;
            return View(user);

        }

        // GET: UserProfiles/Create

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(UserProfile createProfile, int personType, IFormFile profilePic)
        {
            bool isLegalPerson = personType == 0 ? false : true;
            if (ModelState.IsValid)
            {
                createProfile.ProfilePic = _imageService.ConvertImgToString(profilePic);
                if (isLegalPerson)
                {
                    createProfile.NaturalPerson = null;
                    createProfile.TotalSlotsAd = Constants.LegalPersonFreeSlots;
                }
                else
                {
                    createProfile.LegalPerson = null;
                    createProfile.TotalSlotsAd = Constants.NaturalPersonFreeSlots;
                }

                bool created = false;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                created = await _userProfileService.CreateProfile(createProfile, userId);

                if (created) return RedirectToAction("SetInterests");

            }
            return View(createProfile);
        }
        public async Task<IActionResult> SetInterests()
        {
            return View(await _categoryService.GetCategories());
        }
        [HttpPost]
        public async Task<IActionResult> SetInterests(Guid[] interests)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userProfileService.SetInterests(userId, interests);


            return RedirectToAction("Details", "UserProfiles");
        }
    }
}
