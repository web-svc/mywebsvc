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
    public class AppsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Apps
        public async Task<IActionResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var applicationDbContext2 = _context.Apps.Where(s => s.Name == searchString || s.Secret == searchString || s.TagLine == searchString || s.User.UserName == searchString).Include(a => a.User);
                return View(await applicationDbContext2.ToListAsync());
            }

            var applicationDbContext = _context.Apps.Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Apps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Apps == null)
            {
                return NotFound();
            }

            var app = await _context.Apps
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // GET: Apps/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Apps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Secret,UserId,Name,TagLine,LogoUrl,RedirectUrl")] App app)
        {
            if (ModelState.IsValid)
            {
                _context.Add(app);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("error", "This Secret Is Used Before , Please Use Another One");
                    return View(app);
                }
                return RedirectToAction(nameof(Index));

            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", app.UserId);
            return View(app);
        }

        // GET: Apps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Apps == null)
            {
                return NotFound();
            }

            var app = await _context.Apps.FindAsync(id);
            if (app == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "I", app.UserId);
            return View(app);
        }

        // POST: Apps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Secret,UserId,Name,TagLine,LogoUrl,RedirectUrl")] App app)
        {
            if (id != app.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(app);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppExists(app.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", app.UserId);
            return View(app);
        }

        // GET: Apps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Apps == null)
            {
                return NotFound();
            }

            var app = await _context.Apps
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // POST: Apps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Apps == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Apps'  is null.");
            }
            var app = await _context.Apps.FindAsync(id);
            if (app != null)
            {
                _context.Apps.Remove(app);
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

        private bool AppExists(int id)
        {
            return (_context.Apps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
