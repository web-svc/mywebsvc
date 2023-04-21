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
    public class AuthServersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthServersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AuthServers
        public async Task<IActionResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var applicationDbContext2 = _context.AuthServers.Where(s => s.Key ==searchString || s.Secret == searchString || s.App.Name == searchString || s.Router.Name == searchString).Include(a => a.App).Include(a => a.Router);
                return View(await applicationDbContext2.ToListAsync());
            }
            var applicationDbContext = _context.AuthServers.Include(a => a.App).Include(a => a.Router);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AuthServers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AuthServers == null)
            {
                return NotFound();
            }

            var authServer = await _context.AuthServers
                .Include(a => a.App)
                .Include(a => a.Router)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authServer == null)
            {
                return NotFound();
            }

            return View(authServer);
        }

        // GET: AuthServers/Create
        public IActionResult Create()
        {
            ViewData["AppId"] = new SelectList(_context.Apps, "Id", "Name");
            ViewData["RouterId"] = new SelectList(_context.Routers, "Id", "Name");
            return View();
        }

        // POST: AuthServers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppId,RouterId,Key,Secret")] AuthServer authServer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authServer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppId"] = new SelectList(_context.Apps, "Id", "Name", authServer.AppId);
            ViewData["RouterId"] = new SelectList(_context.Routers, "Id", "Name", authServer.RouterId);
            return View(authServer);
        }

        // GET: AuthServers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AuthServers == null)
            {
                return NotFound();
            }

            var authServer = await _context.AuthServers.FindAsync(id);
            if (authServer == null)
            {
                return NotFound();
            }
            ViewData["AppId"] = new SelectList(_context.Apps, "Id", "Name", authServer.AppId);
            ViewData["RouterId"] = new SelectList(_context.Routers, "Id", "Name", authServer.RouterId);
            return View(authServer);
        }

        // POST: AuthServers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppId,RouterId,Key,Secret")] AuthServer authServer)
        {
            if (id != authServer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authServer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthServerExists(authServer.Id))
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
            ViewData["AppId"] = new SelectList(_context.Apps, "Id", "Name", authServer.AppId);
            ViewData["RouterId"] = new SelectList(_context.Routers, "Id", "Name", authServer.RouterId);
            return View(authServer);
        }

        // GET: AuthServers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AuthServers == null)
            {
                return NotFound();
            }

            var authServer = await _context.AuthServers
                .Include(a => a.App)
                .Include(a => a.Router)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authServer == null)
            {
                return NotFound();
            }

            return View(authServer);
        }

        // POST: AuthServers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AuthServers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AuthServers'  is null.");
            }
            var authServer = await _context.AuthServers.FindAsync(id);
            if (authServer != null)
            {
                _context.AuthServers.Remove(authServer);
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

        private bool AuthServerExists(int id)
        {
          return (_context.AuthServers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
