using CarDealerApp.Data;
using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealerApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /Cars
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars.ToListAsync();
            return View(cars); // Views/Cars/Index.cshtml
        }

        // GET /Cars/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            var inquiries = await _context.Inquiries
                .Where(i => i.CarId == id)
                .ToListAsync();

            ViewBag.Inquiries = inquiries;
            return View(car); // Views/Cars/Details.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> SubmitInquiry(int carId, string name, string email, string message)
        {
            var inquiry = new Inquiry
            {
                CarId = carId,
                Name = name,
                Email = email,
                Message = message
            };

            _context.Inquiries.Add(inquiry);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Inquiry submitted successfully!";
            return RedirectToAction("Details", new { id = carId });
        }
    }
}