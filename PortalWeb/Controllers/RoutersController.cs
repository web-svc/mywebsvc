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
    public class RoutersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoutersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Routers
        public async Task<IActionResult> Index(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                var applicationDbContext2 = _context.Routers.Where(s => s.Name == searchString );
                return View(await applicationDbContext2.ToListAsync());
            }
            return _context.Routers != null ? 
                          View(await _context.Routers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Routers'  is null.");
        }

        // GET: Routers/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.Routers == null)
            {
                return NotFound();
            }

            var router = await _context.Routers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (router == null)
            {
                return NotFound();
            }

            return View(router);
        }

        // GET: Routers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Routers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Router router)
        {
            if (ModelState.IsValid)
            {
                _context.Add(router);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(router);
        }

        // GET: Routers/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Routers == null)
            {
                return NotFound();
            }

            var router = await _context.Routers.FindAsync(id);
            if (router == null)
            {
                return NotFound();
            }
            return View(router);
        }

        // POST: Routers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Name")] Router router)
        {
            if (id != router.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(router);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouterExists(router.Id))
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
            return View(router);
        }

        // GET: Routers/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Routers == null)
            {
                return NotFound();
            }

            var router = await _context.Routers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (router == null)
            {
                return NotFound();
            }

            return View(router);
        }

        // POST: Routers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.Routers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Routers'  is null.");
            }
            var router = await _context.Routers.FindAsync(id);
            if (router != null)
            {
                _context.Routers.Remove(router);
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

        private bool RouterExists(byte id)
        {
          return (_context.Routers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
