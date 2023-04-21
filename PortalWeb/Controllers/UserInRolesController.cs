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
    public class UserInRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserInRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserInRoles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserInRoles.Include(u => u.Role).Include(u => u.User);

           

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserInRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserInRoles == null)
            {
                return NotFound();
            }

            var userInRole = await _context.UserInRoles
                .Include(u => u.Role)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInRole == null)
            {
                return NotFound();
            }

            return View(userInRole);
        }

        // GET: UserInRoles/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: UserInRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,RoleId")] UserInRole userInRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userInRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", userInRole.UserId);
            return View(userInRole);
        }

        // GET: UserInRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserInRoles == null)
            {
                return NotFound();
            }

            var userInRole = await _context.UserInRoles.FindAsync(id);
            if (userInRole == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userInRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", userInRole.UserId);
            return View(userInRole);
        }

        // POST: UserInRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,RoleId")] UserInRole userInRole)
        {
            if (id != userInRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInRoleExists(userInRole.Id))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userInRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", userInRole.UserId);
            return View(userInRole);
        }

        // GET: UserInRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserInRoles == null)
            {
                return NotFound();
            }

            var userInRole = await _context.UserInRoles
                .Include(u => u.Role)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInRole == null)
            {
                return NotFound();
            }

            return View(userInRole);
        }

        // POST: UserInRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserInRoles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserInRoles'  is null.");
            }
            var userInRole = await _context.UserInRoles.FindAsync(id);
            if (userInRole != null)
            {
                _context.UserInRoles.Remove(userInRole);
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

        private bool UserInRoleExists(int id)
        {
          return (_context.UserInRoles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
