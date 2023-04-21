using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortalWeb.Data;
using PortalWeb.Models;

namespace PortalWeb.Controllers
{
    public class StatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: States
        public async Task<IActionResult> Index(string searchString)
        {
            var applicationDbContext = _context.States.Include(s => s.Country);

            if (!String.IsNullOrEmpty(searchString))
            {
                var applicationDbContext2 = _context.States.Where(s => s.Name == searchString || s.Country.Name == searchString || s.StateId.ToString() == searchString ).Include(s => s.Country);
                return View(await applicationDbContext2.ToListAsync());
            }

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: States/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // GET: States/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: States/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StateId,Name,CountryId")] State state)
        {
            if (ModelState.IsValid)
            {
                _context.Add(state);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", state.CountryId);
            return View(state);
        }

        // GET: States/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", state.CountryId);
            return View(state);
        }

        // POST: States/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StateId,Name,CountryId")] State state)
        {
            if (id != state.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateExists(state.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", state.CountryId);
            return View(state);
        }

        // GET: States/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.States == null)
            {
                return Problem("Entity set 'ApplicationDbContext.States'  is null.");
            }
            var state = await _context.States.FindAsync(id);
            if (state != null)
            {
                _context.States.Remove(state);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("error", "This Is Used By Another Entity Soo You Can't Delete It");
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool StateExists(int id)
        {
          return (_context.States?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
