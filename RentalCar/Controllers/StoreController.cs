﻿using System;
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
    public class StoreController : Controller
    {
        private readonly DatabaseContext _context;

        public StoreController(DatabaseContext context)
        {
            _context = context;
        }

        [Route("store")]
        public async Task<IActionResult> Index()
        {
            var models = await _context.Stores.Where(m => m.IsDeleted == false).OrderBy(m => m.Id).ToListAsync();
            return View(models);
        }

        [Route("store-detail")]
        public async Task<IActionResult> Detail(int? id)
        {
            var model = await _context.Stores.FindAsync(id) ?? new Store();
            return View(model);
        }

        [Route("store-detail")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(Store model)
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
            return View(model);
        }

        [Route("store-delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var model = await _context.Stores.FindAsync(id);
            if(model == null)
            {
                return RedirectToAction(nameof(NotFound));
            }
            return View(model);
        }

        [Route("store-delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)

        {
            var model = await _context.Stores.FindAsync(id);
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
