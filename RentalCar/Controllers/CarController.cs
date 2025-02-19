using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using RentalCar.Data;
using RentalCar.Models;

namespace RentalCar.Controllers
{
    public class CarController : Controller
    {
        private readonly DatabaseContext _context;

        public CarController(DatabaseContext context)
        {
            _context = context;
        }

        [Route("car")]
        public async Task<IActionResult> Index()
        {
            var models = await _context.Cars.Where(m => m.IsDeleted == false).OrderBy(m => m.Id).ToListAsync();
            return View(models);
        }

        [Route("car-detail")]
        public async Task<IActionResult> Detail(int? id)
        {
            var model = await _context.Cars.FindAsync(id) ?? new Car();
            ViewData["CarClassId"] = new SelectList(_context.CarClasses, "Id", "Name", model.CarClassId);
            ViewData["CarDivisionId"] = new SelectList(_context.CarDivisions, "Id", "Name", model.CarDivisionId);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes.Where(m => m.CarClassId == model.CarClassId), "Id", "Name", model.CarTypeId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name", model.StoreId);
            return View(model);
        }

        [Route("car-detail")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(Car model)
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
            ViewData["CarDivisionId"] = new SelectList(_context.CarDivisions, "Id", "Name", model.CarDivisionId);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes.Where(m => m.CarClassId == model.CarClassId), "Id", "Name", model.CarTypeId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name", model.StoreId);
            return View(model);
        }

        [Route("car-delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var model = await _context.Cars.FindAsync(id);
            if(model == null)
            {
                return RedirectToAction(nameof(NotFound));
            }
            return View(model);
        }

        [Route("car-delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)

        {
            var model = await _context.Cars.FindAsync(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _context.Update(model);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("get-car-type")]
        public IActionResult GetCarType(int? CarClassId)
        {
            var models = _context.CarTypes.IgnoreAutoIncludes().Where(m => m.CarClassId == CarClassId).ToJson();
            return Content(models, "application/json");
        }
    }
}
