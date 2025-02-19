using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RentalCar.ViewModels;

namespace RentalCar.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("page-not-found")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
