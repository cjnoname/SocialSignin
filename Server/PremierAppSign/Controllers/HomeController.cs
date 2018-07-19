using Microsoft.AspNetCore.Mvc;

namespace PremierAppSign.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
