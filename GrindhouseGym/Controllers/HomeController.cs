using GrindhouseGym.Data; // Ensure you have this using statement
using GrindhouseGym.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GrindhouseGym.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; // 1. Declare the database tool

        // 2. Inject the database tool in the constructor
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // ONLY do the math if the user is logged in
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.TotalMembers = _context.Members.Count();
                ViewBag.TotalRevenue = _context.Members.Sum(m => m.Plan.Fee);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}