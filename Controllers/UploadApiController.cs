using CarDealerApp.Data;
using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarDealerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UploadApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("car")]
        public async Task<IActionResult> UploadCar([FromBody] Car car)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Car uploaded successfully!", carId = car.Id });
        }
    }
}
