using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalCar.Application.Database;
using RentalCar.Application.Models;

namespace RentalCar.Application.Controllers
{
    public class CarClasseController : Controller
    {
        private readonly DatabaseContext _context;

        public CarClasseController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: CarClasse
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.CarClasses.Include(c => c.carCategory);
            return View(await databaseContext.ToListAsync());
        }

        // GET: CarClasse/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarClasses == null)
            {
                return NotFound();
            }

            var carClass = await _context.CarClasses
                .Include(c => c.carCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carClass == null)
            {
                return NotFound();
            }

            return View(carClass);
        }

        // GET: CarClasse/Create
        public IActionResult Create()
        {
            ViewData["CarCategoryId"] = new SelectList(_context.CarCategories, "Id", "Name");
            return View();
        }

        // POST: CarClasse/Create   
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CarCategoryId,Description,Remarks,DisplayOrder,Id,CreatedAt,UpdatedAt")] CarClass carClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarCategoryId"] = new SelectList(_context.CarCategories, "Id", "Id", carClass.CarCategoryId);
            return View(carClass);
        }

        // GET: CarClasse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarClasses == null)
            {
                return NotFound();
            }

            var carClass = await _context.CarClasses.FindAsync(id);
            if (carClass == null)
            {
                return NotFound();
            }
            ViewData["CarCategoryId"] = new SelectList(_context.CarCategories, "Id", "Id", carClass.CarCategoryId);
            return View(carClass);
        }

        // POST: CarClasse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,CarCategoryId,Description,Remarks,DisplayOrder,Id,CreatedAt,UpdatedAt")] CarClass carClass)
        {
            if (id != carClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarClassExists(carClass.Id))
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
            ViewData["CarCategoryId"] = new SelectList(_context.CarCategories, "Id", "Id", carClass.CarCategoryId);
            return View(carClass);
        }

        // GET: CarClasse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarClasses == null)
            {
                return NotFound();
            }

            var carClass = await _context.CarClasses
                .Include(c => c.carCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carClass == null)
            {
                return NotFound();
            }

            return View(carClass);
        }

        // POST: CarClasse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarClasses == null)
            {
                return Problem("Entity set 'DatabaseContext.CarClasses'  is null.");
            }
            var carClass = await _context.CarClasses.FindAsync(id);
            if (carClass != null)
            {
                _context.CarClasses.Remove(carClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarClassExists(int id)
        {
            return (_context.CarClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
