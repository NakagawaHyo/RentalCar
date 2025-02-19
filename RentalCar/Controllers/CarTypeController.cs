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
    public class CarTypeController : Controller
    {
        private readonly DatabaseContext _context;

        public CarTypeController(DatabaseContext context)
        {
            _context = context;
        }

        [Route("cartype")]
        public async Task<IActionResult> Index()
        {
            var models = await _context.CarTypes.Where(m => m.IsDeleted == false).OrderBy(m => m.DisplayOrder).ToListAsync();
            return View(models);
        }

        [Route("cartype-detail")]
        public async Task<IActionResult> Detail(int? id)
        {
            var model = await _context.CarTypes.FindAsync(id) ?? new CarType();
            ViewData["CarClassId"] = new SelectList(_context.CarClasses, "Id", "Name", model.CarClassId);
            return View(model);
        }

        [Route("cartype-detail")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(CarType model)
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
            ViewData["CarClassId"] = new SelectList(_context.CarClasses, "Id", "Name", model.CarClassId);
            return View(model);
        }

        [Route("cartype-delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var model = await _context.CarTypes.FindAsync(id);
            if(model == null)
            {
                return RedirectToAction(nameof(NotFound));
            }
            return View(model);
        }

        [Route("cartype-delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)

        {
            var model = await _context.CarTypes.FindAsync(id);
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
