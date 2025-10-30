using Microsoft.AspNetCore.Mvc;

namespace CarDealerApp.Controllers
{
    public class InquiriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
