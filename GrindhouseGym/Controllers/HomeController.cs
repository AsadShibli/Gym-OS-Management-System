using GrindhouseGym.Data;
using GrindhouseGym.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Required for .Include()
using System.Diagnostics;

namespace GrindhouseGym.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Changed to 'async Task' for database efficiency
        public async Task<IActionResult> Index()
        {
            // ONLY do the math if the user is logged in (Admin Dashboard)
            if (User.Identity.IsAuthenticated)
            {
                // 1. Fetch all members AND load their Plan data (Critical step!)
                var members = await _context.Members
                                            .Include(m => m.Plan)
                                            .ToListAsync();

                decimal calculatedRevenue = 0;

                // 2. Loop through every member to calculate their specific payment
                foreach (var member in members)
                {
                    // Safety check: Ensure they have a plan assigned
                    if (member.Plan != null)
                    {
                        // Base Calculation: Fee * Months
                        decimal grossAmount = member.Plan.Fee * member.Duration;

                        // Apply Strategic Discounts
                        if (member.Duration == 6)
                        {
                            // 10% Discount for 6-month commitment
                            grossAmount = grossAmount * 0.90m;
                        }
                        else if (member.Duration == 12)
                        {
                            // 20% Discount for 1-year commitment
                            grossAmount = grossAmount * 0.80m;
                        }

                        calculatedRevenue += grossAmount;
                    }
                }

                // 3. Send data to the View
                ViewBag.TotalMembers = members.Count;
                ViewBag.TotalRevenue = calculatedRevenue;
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