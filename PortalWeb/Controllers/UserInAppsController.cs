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
    public class UserInAppsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserInAppsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserInApps
        public async Task<IActionResult> Index(string searchString)
        {
            var applicationDbContext = _context.UserInApps.Include(u => u.App).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserInApps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserInApps == null)
            {
                return NotFound();
            }

            var userInApp = await _context.UserInApps
                .Include(u => u.App)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInApp == null)
            {
                return NotFound();
            }

            return View(userInApp);
        }

        // GET: UserInApps/Create
        public IActionResult Create()
        {
            ViewData["AppId"] = new SelectList(_context.Apps, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: UserInApps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,AppId")] UserInApp userInApp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInApp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppId"] = new SelectList(_context.Apps, "Id", "Name", userInApp.AppId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", userInApp.UserId);
            return View(userInApp);
        }

        // GET: UserInApps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserInApps == null)
            {
                return NotFound();
            }

            var userInApp = await _context.UserInApps.FindAsync(id);
            if (userInApp == null)
            {
                return NotFound();
            }
            ViewData["AppId"] = new SelectList(_context.Apps, "Id", "Name", userInApp.AppId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", userInApp.UserId);
            return View(userInApp);
        }

        // POST: UserInApps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,AppId")] UserInApp userInApp)
        {
            if (id != userInApp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInApp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInAppExists(userInApp.Id))
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
            ViewData["AppId"] = new SelectList(_context.Apps, "Id", "Name", userInApp.AppId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", userInApp.UserId);
            return View(userInApp);
        }

        // GET: UserInApps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserInApps == null)
            {
                return NotFound();
            }

            var userInApp = await _context.UserInApps
                .Include(u => u.App)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInApp == null)
            {
                return NotFound();
            }

            return View(userInApp);
        }

        // POST: UserInApps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserInApps == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserInApps'  is null.");
            }
            var userInApp = await _context.UserInApps.FindAsync(id);
            if (userInApp != null)
            {
                _context.UserInApps.Remove(userInApp);
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

        private bool UserInAppExists(int id)
        {
          return (_context.UserInApps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
