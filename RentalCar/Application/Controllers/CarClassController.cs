using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalCar.Application.Database;
using RentalCar.Application.Models;
using ILogger = Spark.Library.Logging.ILogger;

namespace RentalCar.Application.Controllers
{
    public class CarClassController : Controller
    {
        private readonly ILogger _logger;
        private readonly DatabaseContext _db;

        public CarClassController(ILogger logger, DatabaseContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var result = _db.CarClasses.Include(c => c.carCategory);
            return View(await result.ToListAsync());
        }

        public IActionResult Create(int? id)
        {
            var carClass = _db.CarClasses.Find(id);
            ViewData["CarCategoryId"] = new SelectList(_db.CarCategories, "Id", "Name", carClass?.CarCategoryId);
            return View(carClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CarCategoryId,Description,Remarks,DisplayOrder,Id,CreatedAt,UpdatedAt")] CarClass carClass)
        {
            if (ModelState.IsValid)
            {
                _db.Add(carClass);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarCategoryId"] = new SelectList(_db.CarCategories, "Id", "Name", carClass.CarCategoryId);
            return View(carClass);
        }

        // GET: CarClasse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.CarClasses == null)
            {
                return NotFound();
            }

            var carClass = await _db.CarClasses
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
            if (_db.CarClasses == null)
            {
                return Problem("Entity set 'DatabaseContext.CarClasses'  is null.");
            }
            var carClass = await _db.CarClasses.FindAsync(id);
            if (carClass != null)
            {
                _db.CarClasses.Remove(carClass);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
