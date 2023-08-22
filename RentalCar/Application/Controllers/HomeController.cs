using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Database;
using RentalCar.Application.ViewModels;
using System.Diagnostics;
using ILogger = Spark.Library.Logging.ILogger;

namespace RentalCar.Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly DatabaseContext _db;

        public HomeController(ILogger logger, DatabaseContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var result = _db.CarClasses.ToList();
            return View();
        }

        [Authorize]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}