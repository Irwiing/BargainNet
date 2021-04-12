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

namespace BargainNet.WebApp.Controllers
{
    public class UserProfilesController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly ICategoryService _categoryService;
        public UserProfilesController(IUserProfileService userProfileService, 
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _userProfileService = userProfileService;
        }

        // GET: UserProfiles
        public async Task<IActionResult> Index()
        {
            return NotFound();
        }

        // GET: UserProfiles/Details/5
        public async Task<IActionResult> Details(string userName)
        {
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

        // POST: UserProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(CreateProfileView createProfile, string[] interests, bool isLegalPerson)
        {
            if (ModelState.IsValid)
            {
                bool created = false;
                var userName = User.Identity;
                List<Category> interestCategories = new List<Category>();
                foreach (var interest in interests)
                {
                    interestCategories.Add(await _categoryService.GetCategory(interest));
                }
                if (isLegalPerson)
                {
                    var legalPerson = new LegalPerson
                    {
                        Document = createProfile.Document,
                        ProfilePic = createProfile.ProfilePic,
                        Address = createProfile.Address,
                        CompanyName = createProfile.CompanyName,
                        TradeName = createProfile.TradeName,
                        DocumentPic = createProfile.Document,
                        Interests = interestCategories
                    };

                    created = await _userProfileService.CreateProfile(legalPerson, userName.Name);
                }
                else
                {
                    var naturalPerson = new NaturalPerson
                    {
                        Document = createProfile.Document,
                        ProfilePic = createProfile.ProfilePic,
                        Address = createProfile.Address,
                        FullName = createProfile.FullName,
                        BirthDate = createProfile.BirthDate,
                        Interests = interestCategories
                    };
                    created = await _userProfileService.CreateProfile(naturalPerson, userName.Name);
                }

                if(created) return RedirectToAction(nameof(Details), new { userName = userName.Name });

            }
            return View(createProfile);
        }


        // GET: UserProfiles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            return NotFound();
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Document,ProfilePic,BarganhaPoints,TotalSlotsAd,Status,Id")] UserProfile userProfile)
        {
            return NotFound();
        }

        // GET: UserProfiles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            return NotFound();
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            return NotFound();
        }

        private bool UserProfileExists(Guid id)
        {
            return false;
        }
    }
}
