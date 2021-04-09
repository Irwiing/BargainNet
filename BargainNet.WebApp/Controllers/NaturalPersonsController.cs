using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BargainNet.Core.Entities;
using BargainNet.Infra.SQL.Data;

namespace BargainNet.WebApp.Controllers
{
    public class NaturalPersonsController : Controller
    {
        private readonly DataContext _context;

        public NaturalPersonsController(DataContext context)
        {
            _context = context;
        }

        // GET: NaturalPersons
        public async Task<IActionResult> Index()
        {
            return View(await _context.NaturalPersons.ToListAsync());
        }

        // GET: NaturalPersons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naturalPerson = await _context.NaturalPersons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (naturalPerson == null)
            {
                return NotFound();
            }

            return View(naturalPerson);
        }

        // GET: NaturalPersons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NaturalPersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,BirthDate,Document,ProfilePic,BarganhaPoints,TotalSlotsAd,Status,Id")] NaturalPerson naturalPerson)
        {
            if (ModelState.IsValid)
            {
                naturalPerson.Id = Guid.NewGuid();
                _context.Add(naturalPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(naturalPerson);
        }

        // GET: NaturalPersons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naturalPerson = await _context.NaturalPersons.FindAsync(id);
            if (naturalPerson == null)
            {
                return NotFound();
            }
            return View(naturalPerson);
        }

        // POST: NaturalPersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FullName,BirthDate,Document,ProfilePic,BarganhaPoints,TotalSlotsAd,Status,Id")] NaturalPerson naturalPerson)
        {
            if (id != naturalPerson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(naturalPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NaturalPersonExists(naturalPerson.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(naturalPerson);
        }

        // GET: NaturalPersons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naturalPerson = await _context.NaturalPersons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (naturalPerson == null)
            {
                return NotFound();
            }

            return View(naturalPerson);
        }

        // POST: NaturalPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var naturalPerson = await _context.NaturalPersons.FindAsync(id);
            _context.NaturalPersons.Remove(naturalPerson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NaturalPersonExists(Guid id)
        {
            return _context.NaturalPersons.Any(e => e.Id == id);
        }
    }
}
