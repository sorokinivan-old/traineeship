using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASPNETProject.Models;

namespace ASPNETProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DBContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            ViewBag.CheckConnection = db.Database.CanConnect();
            return View();
        }

    }
}
