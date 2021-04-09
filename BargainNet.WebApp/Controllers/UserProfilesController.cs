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

namespace BargainNet.WebApp.Controllers
{
    public class UserProfilesController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly DataContext _context;

        public UserProfilesController(IUserProfileService userProfileService, DataContext context)
        {
            _userProfileService = userProfileService;
            _context = context;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Document,ProfilePic,FullName,BirthDate")] NaturalPerson userProfile)
        {
            if (ModelState.IsValid)
            {
                
                var userName = User.Identity;
                var id = ((ClaimsIdentity)userName).FindFirst(ClaimTypes.NameIdentifier);
                
                if (await _userProfileService.CreateProfile(userProfile, userName.Name))
                {
                    return RedirectToAction(nameof(Details), new { userName = userName.Name });

                }

            }
            return View(userProfile);
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
