using BargainNet.Core.Contracts.Services;
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
        public ActionResult CreateOffer(Guid idAuction)
        {
            ViewData["idAuction"] = idAuction;
            return View("_CreateOffer");
        }
        [HttpPost]
        
        public async Task<ActionResult> CreateOffer(Offer offer, Guid idAuction)
        {
            await _auctionService.CreateOffer(offer, idAuction);
            return RedirectToAction("Details", new { id = idAuction });
        }
        // GET: AuctionsController/Create
        public async Task<ActionResult> Create()
        {
            var userName = User.Identity.Name;
            if (await _userProfileService.HasSlots(userName))
            {
                var categories = await _categoryService.GetCategories();
                ViewData["Categories"] = categories;
                return View();
            }
            return RedirectToAction("Details", "UserProfiles", new { userName });
        }

        // POST: AuctionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdAuction auction, IFormFile adPic)
        {
            var userName = User.Identity.Name;
            var category = await _categoryService.GetCategory(auction.Category.Id);
            auction.Category = category;
            auction.AdPic = _imageService.ConvertImgToString(adPic);
            await _auctionService.CreateAuction(userName, auction);

            return RedirectToAction("Details", "UserProfiles", new { userName });

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

        // GET: AuctionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuctionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
