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
using Microsoft.AspNetCore.Http;

namespace BargainNet.WebApp.Controllers
{
    [Authorize]
    public class UserProfilesController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly ICategoryService _categoryService;
        
        private readonly IAuctionService _auctionService;
        public UserProfilesController(IUserProfileService userProfileService,
            ICategoryService categoryService,
            IAuctionService auctionService)
        {
            _categoryService = categoryService;
            _userProfileService = userProfileService;
            _auctionService = auctionService;
        }
        
        public async Task<IActionResult> Details()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userProfileService.GetProfile(userId);
           
            if (user.UserProfile == null)
            {
                return RedirectToAction("Create");
            }
            if (!user.UserProfile.Interests.Any())
            {
                return RedirectToAction("SetInterests");
            }
            
            return View(user);

        }
        public async Task<IActionResult> CheckNotify()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userProfileService.GetProfile(userId);
            if (user.UserProfile == null)
            {
                return BadRequest();
            }
            if (!user.UserProfile.Interests.Any())
            {
                return BadRequest();
            }
            var auctions = await _auctionService.GetUserInterestAuctions(userId);
            return Ok(auctions.Count);
        }
    
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(IFormFile profilePic, IFormFile documentPic, int personType, UserProfile createProfile)
        {
           
            if (ModelState.IsValid)
            {
                
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                try
                {
                    
                   await _userProfileService.CreateProfile(createProfile, userId, personType, profilePic, documentPic);
                }
                catch (Exception e)
                {

                    ViewData["ErroMessage"] = e.Message;
                    return View(createProfile);
                }

                return RedirectToAction("SetInterests");
            }
            return View(createProfile);
        }
        public async Task<IActionResult> SetInterests()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);    
            return View(await _userProfileService.GetInterests(userId));
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
