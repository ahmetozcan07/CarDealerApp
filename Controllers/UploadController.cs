using CarDealerApp.Data;
using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarDealerApp.Controllers
{
    public class UploadController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UploadController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Admin/Upload"); // Views/Upload/Upload.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Index(Car car, IFormFile? image)
        {
            if (!ModelState.IsValid)
                return View("Admin/Upload", car);

            if (image != null && image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(fileStream);

                car.ImagePath = "/uploads/" + fileName;
            }

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            ViewBag.Message = "Car uploaded successfully!";
            return View("Admin/Upload");
        }
    }
}