using Ignition.Common.Attribute;
using Microsoft.AspNetCore.Mvc;
using PremierAppSign.Interfaces;
using PremierAppSign.Models.Ignition;

namespace PremierAppSign.Controllers
{
    [AwsRequestCapture]
    [AccessTokenRequired]
    [MonitoredRequest]
    [XAPIKey]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IIgnitionService _ignitionService;

        public AccountController(IIgnitionService ignitionService)
        {
            _ignitionService = ignitionService;
        }

        [HttpPost("[action]")]
        public IActionResult SignIn([FromBody]SignInRequest signInRequest)
        {
            if (ModelState.IsValid)
            {
                return Ok(_ignitionService.SignIn(signInRequest));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
