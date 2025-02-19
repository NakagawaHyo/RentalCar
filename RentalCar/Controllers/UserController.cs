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
    public class UserController : Controller
    {
        private readonly DatabaseContext _context;

        public UserController(DatabaseContext context)
        {
            _context = context;
        }

        [Route("user")]
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.Where(m => m.IsDeleted == false).OrderBy(m => m.Id).ToListAsync();
            return View(users);
        }

        [Route("user-detail")]
        public async Task<IActionResult> Detail(int? id)
        {
            var user = await _context.Users.FindAsync(id) ?? new User();
            ViewData["StoreId"] = new SelectList(_context.Set<Store>(), "Id", "Name", user.StoreId);
            return View(user);
        }

        [Route("user-detail")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Id == 0)
                {
                    _context.Add(user);
                }
                else
                {
                    _context.Update(user);
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreId"] = new SelectList(_context.Set<Store>(), "Id", "Name", user.StoreId);
            return View(user);
        }

        [Route("user-delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return RedirectToAction(nameof(NotFound));
            }
            return View(user);
        }

        [Route("user-delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)

        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsDeleted = true;
                _context.Update(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
