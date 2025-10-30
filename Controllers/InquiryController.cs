using CarDealerApp.Data;
using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealerApp.Controllers
{
    public class InquiryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InquiryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null) return NotFound();

            var inquiries = await _context.Inquiries
                .Where(i => i.CarId == carId)
                .ToListAsync();

            ViewBag.Car = car;
            return View(inquiries);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitInquiry(Inquiry inquiry)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = inquiry.CarId });
            }

            _context.Inquiries.Add(inquiry);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Inquiry submitted successfully!";
            return RedirectToAction("Details", new { id = inquiry.CarId });
        }
    }
}