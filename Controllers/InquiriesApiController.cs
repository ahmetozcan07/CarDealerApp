using CarDealerApp.Data;
using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InquiriesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InquiriesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInquiry([FromBody] Inquiry inquiry)
        {
            _context.Inquiries.Add(inquiry);
            await _context.SaveChangesAsync();
            return Ok(inquiry);
        }

        [HttpGet]
        public async Task<IActionResult> GetInquiries()
        {
            var list = await _context.Inquiries
                .Include(i => i.Car)
                .ToListAsync();
            return Ok(list);
        }
    }
}
