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
    public class ClientController : Controller
    {
        private readonly DatabaseContext _context;

        public ClientController(DatabaseContext context)
        {
            _context = context;
        }

        [Route("client")]
        public async Task<IActionResult> Index()
        {
            var models = await _context.Clients.Where(m => m.IsDeleted == false).OrderBy(m => m.Id).ToListAsync();
            return View(models);
        }

        [Route("client-detail")]
        public async Task<IActionResult> Detail(int? id)
        {
            var model = await _context.Clients.FindAsync(id) ?? new Client();
            return View(model);
        }

        [Route("client-detail")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(Client model)
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

        [Route("client-delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var model = await _context.Clients.FindAsync(id);
            if(model == null)
            {
                return RedirectToAction(nameof(NotFound));
            }
            return View(model);
        }

        [Route("client-delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)

        {
            var model = await _context.Clients.FindAsync(id);
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
