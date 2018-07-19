using Ignition.Common.Attribute;
using Microsoft.AspNetCore.Mvc;
using PremierAppSign.Interfaces;

namespace PremierAppSign.Controllers
{
    [AwsRequestCapture]
    [AccessTokenRequired]
    [MonitoredRequest]
    [XAPIKey]
    public class HomeController : Controller
    {
        private readonly IIgnitionService _ignitionService;

        public HomeController(IIgnitionService ignitionService)
        {
            _ignitionService = ignitionService;
        }

        public IActionResult Index()
        {
            ViewBag.Path = Request.Path.Value;
            ViewBag.Initial = _ignitionService.GetConfig();
            return View();
        }
    }
}
