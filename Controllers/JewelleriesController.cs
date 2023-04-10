using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Adornian.Data;
using Adornian.Models;

namespace Adornian.Controllers
{
    public class JewelleriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JewelleriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jewelleries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Jewellery.Include(j => j.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Jewelleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jewellery == null)
            {
                return NotFound();
            }

            var jewellery = await _context.Jewellery
                .Include(j => j.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jewellery == null)
            {
                return NotFound();
            }

            return View(jewellery);
        }

        // GET: Jewelleries/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View();
        }

        // POST: Jewelleries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryId")] Jewellery jewellery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jewellery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", jewellery.CategoryId);
            return View(jewellery);
        }

        // GET: Jewelleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jewellery == null)
            {
                return NotFound();
            }

            var jewellery = await _context.Jewellery.FindAsync(id);
            if (jewellery == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", jewellery.CategoryId);
            return View(jewellery);
        }

        // POST: Jewelleries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CategoryId")] Jewellery jewellery)
        {
            if (id != jewellery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jewellery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JewelleryExists(jewellery.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", jewellery.CategoryId);
            return View(jewellery);
        }

        // GET: Jewelleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jewellery == null)
            {
                return NotFound();
            }

            var jewellery = await _context.Jewellery
                .Include(j => j.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jewellery == null)
            {
                return NotFound();
            }

            return View(jewellery);
        }

        // POST: Jewelleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jewellery == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Jewellery'  is null.");
            }
            var jewellery = await _context.Jewellery.FindAsync(id);
            if (jewellery != null)
            {
                _context.Jewellery.Remove(jewellery);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JewelleryExists(int id)
        {
          return _context.Jewellery.Any(e => e.Id == id);
        }
    }
}
