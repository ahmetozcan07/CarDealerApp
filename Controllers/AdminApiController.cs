using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminApiController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AdminApiController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Admin admin)
        {
            var username = _config["Admin:Username"];
            var password = _config["Admin:Password"];

            if (admin.Username == username && admin.Password == password)
                return Ok(new { message = "Login successful" });

            return Unauthorized(new { message = "Invalid credentials" });
        }
    }
}
