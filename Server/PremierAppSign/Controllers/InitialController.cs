using Ignition.Common.Attribute;
using Microsoft.AspNetCore.Mvc;
using PremierAppSign.Interfaces;

namespace PremierAppSign.Controllers
{
    [AwsRequestCapture]
    [AccessTokenRequired]
    [MonitoredRequest]
    [XAPIKey]
    [Route("api/[controller]")]
    public class InitialController : Controller
    {
        private readonly IIgnitionService _ignitionService;

        public InitialController(IIgnitionService ignitionService)
        {
            _ignitionService = ignitionService;
        }

        [HttpGet("[action]")]
        public IActionResult GetInitialValues()
        {
            return Ok(_ignitionService.GetConfig());
        }
    }
}