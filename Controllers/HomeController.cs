using Microsoft.AspNetCore.Mvc;

namespace CarDealerApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
