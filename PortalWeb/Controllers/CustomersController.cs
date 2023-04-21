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
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var applicationDbContext2 = _context.Customers.Where(s => s.Name == searchString || s.Company == searchString 
                || s.City == searchString || s.ZipCode.ToString() == searchString || s.Phone.ToString() == searchString 
                || s.Description == searchString || s.Country.Name == searchString || s.User.UserName == searchString).Include(c => c.Country).Include(c => c.State).Include(c => c.User);
                return View(await applicationDbContext2.ToListAsync());
            }
            var applicationDbContext = _context.Customers.Include(c => c.Country).Include(c => c.State).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Country)
                .Include(c => c.State)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Name,Company,CountryId,StateId,City,Address,ZipCode,Phone,Mobile,Description")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("error", "This User Is Used Before");
                    ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", customer.CountryId);
                    ViewData["StateId"] = new SelectList(_context.States, "Id", "Name", customer.StateId);
                    ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", customer.UserId);
                    return View(customer);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", customer.CountryId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name", customer.StateId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", customer.UserId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", customer.CountryId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name", customer.StateId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", customer.UserId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Name,Company,CountryId,StateId,City,Address,ZipCode,Phone,Mobile,Description")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        ModelState.AddModelError("error", "This User Is Used By Another Customer Soo You Can't Choose It");
                        ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", customer.CountryId);
                        ViewData["StateId"] = new SelectList(_context.States, "Id", "Name", customer.StateId);
                        ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", customer.UserId);
                        return View(customer);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", customer.CountryId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name", customer.StateId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", customer.UserId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Country)
                .Include(c => c.State)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            
            if (customer != null)
            {
                _context.Customers.Remove(customer);
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

        private bool CustomerExists(int id)
        {
          return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
