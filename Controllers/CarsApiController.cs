using CarDealerApp.Data;
using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CarsApiController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            return Ok(await _context.Cars.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();
            return Ok(car);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> AddCar([FromForm] string make, [FromForm] string model,
                                               [FromForm] int year, [FromForm] decimal price,
                                               IFormFile? image)
        {
            string? imagePath = null;

            if (image != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = $"{Guid.NewGuid()}_{image.FileName}";
                string filePath = Path.Combine(uploadsFolder, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(fileStream);

                imagePath = $"/uploads/{fileName}";
            }

            var car = new Car
            {
                Make = make,
                Model = model,
                Year = year,
                Price = price,
                ImagePath = imagePath
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return Ok(car);
        }
    }
}