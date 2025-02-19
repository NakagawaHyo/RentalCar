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
            var stores = await _context.Stores.Where(m => m.IsDeleted == false).OrderBy(m => m.Id).ToListAsync();
            return View(stores);
        }

        [Route("store-detail")]
        public async Task<IActionResult> Detail(int? id)
        {
            var store = await _context.Stores.FindAsync(id) ?? new Store();
            return View(store);
        }

        [Route("store-detail")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(Store store)
        {
            if (ModelState.IsValid)
            {
                if (store.Id == 0)
                {
                    _context.Add(store);
                }
                else
                {
                    _context.Update(store);
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        [Route("store-delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var store = await _context.Stores.FindAsync(id);
            if(store == null)
            {
                return RedirectToAction(nameof(NotFound));
            }
            return View(store);
        }

        [Route("store-delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)

        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                store.IsDeleted = true;
                _context.Update(store);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
