using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models;

namespace RentalCar.Controllers
{
    public class CarClassController : Controller
    {
        private readonly DatabaseContext _context;

        public CarClassController(DatabaseContext context)
        {
            _context = context;
        }

        [Route("carclass")]
        public async Task<IActionResult> Index()
        {
            var models = await _context.CarClasses.Where(m => m.IsDeleted == false).OrderBy(m => m.DisplayOrder).ToListAsync();
            return View(models);
        }

        [Route("carclass-detail")]
        public async Task<IActionResult> Detail(int? id)
        {
            var model = await _context.CarClasses.FindAsync(id) ?? new CarClass();
            ViewData["CarCategoryId"] = new SelectList(_context.CarCategories, "Id", "Name", model.CarCategoryId);
            return View(model);
        }

        [Route("carclass-detail")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(CarClass model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    _context.Add(model);
                }
                else
                {
                    _context.Update(model);
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarCategoryId"] = new SelectList(_context.CarCategories, "Id", "Name", model.CarCategoryId);
            return View(model);
        }

        [Route("carclass-delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var model = await _context.CarClasses.FindAsync(id);
            if(model == null)
            {
                return RedirectToAction(nameof(NotFound));
            }
            return View(model);
        }

        [Route("carclass-delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)

        {
            var model = await _context.CarClasses.FindAsync(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _context.Update(model);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
