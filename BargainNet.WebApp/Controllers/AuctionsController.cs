﻿using BargainNet.Core.Contracts.Services;
using BargainNet.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BargainNet.WebApp.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IAuctionService _auctionService;
        private readonly IUserProfileService _userProfileService;
        private readonly IImageService _imageService;
        public AuctionsController(ICategoryService categoryService,
            IAuctionService auctionService,
            IUserProfileService userProfileService,
            IImageService imageService)
        {
            _categoryService = categoryService;
            _auctionService = auctionService;
            _userProfileService = userProfileService;
            _imageService = imageService;
        }
        // GET: AuctionsController
        public async Task<IActionResult> Index()
        {
            return View(await _auctionService.GetAuctions());
        }

        // GET: AuctionsController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var user = await _userProfileService.GetProfile(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var auction = await _auctionService.GetAuction(id);

            if (user.UserProfile.AdAuctions.Contains(auction)) ViewData["isOwner"] = true;
            else ViewData["isOwner"] = false;
            return View(auction);
        }
        public async Task<ActionResult> ListOffer(Guid id)
        {
            //melhorar para buscar apenas as ofertas da auction
            var auction = await _auctionService.GetAuction(id);
            if (auction == null) return View("_OfferList");
            return View("_OfferList", auction.Offers);
        }
        public ActionResult CreateOffer(Guid id)
        {
            ViewData["idAuction"] = id;
            return View("_CreateOffer");
        }
        [HttpPost]

        public async Task<ActionResult> CreateOffer([Bind("Value")] Offer offer, Guid idAuction)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _auctionService.CreateOffer(offer, idAuction, userId);
            return RedirectToAction("Details", new { id = idAuction });
        }
        public ActionResult EndAuction(Guid idAuction)
        {
            ViewData["idAuction"] = idAuction;
            return View("_EndAuction");
        }
        [HttpPost]
        [ActionName("EndAuction")]
        public async Task<ActionResult> ConfirmEndAuction(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _auctionService.EndAuction(id, userId);
            return RedirectToAction("Details", "UserProfiles");
        }
        // GET: AuctionsController/Create
        public async Task<ActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _userProfileService.HasSlots(userId))
            {
                var categories = await _categoryService.GetCategories();
                ViewData["Categories"] = categories;
                return View();
            }
            return RedirectToAction("Details", "UserProfiles");
        }

        // POST: AuctionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdAuction auction, IFormFile adPic)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var category = await _categoryService.GetCategory(auction.Category.Id);
            auction.Category = category;
            auction.AdPic = _imageService.ConvertImgToString(adPic);
            auction.AdAcutionSettings = new AdAcutionSettings();
            await _auctionService.CreateAuction(userId, auction);

            return RedirectToAction("Details", "UserProfiles");

        }

        // GET: AuctionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AUCtionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}