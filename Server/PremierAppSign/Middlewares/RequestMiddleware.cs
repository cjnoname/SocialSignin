using Ignition.Common.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PremierAppSign.Configurations;
using PremierAppSign.Models.Ignition;
using PremierAppSign.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace PremierAppSign.Middlewares
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly OAuthSettings _oAuthSettings;
        private readonly IgnitionConfigurations _ignitionSettings;

        public RequestMiddleware(RequestDelegate next, ILogger logger, IOptions<OAuthSettings> oAuthSettings, IOptions<IgnitionConfigurations> ignitionSettings)
        {
            _next = next;
            _logger = logger;
            _oAuthSettings = oAuthSettings.Value;
            _ignitionSettings = ignitionSettings.Value;
        }

        public Task Invoke(HttpContext context)
        {
            var clientId = context.Request.Host.Host.Split('.')[0];
            var secret = _oAuthSettings.Clients.SingleOrDefault(x => x.ClientId == clientId)?.Secret;
            if (string.IsNullOrWhiteSpace(secret))
            {
                clientId = "reactapp";
                secret = _oAuthSettings.Clients.Single(x => x.ClientId == clientId).Secret;
            }

            var request = new OAuthRequest
            {
                GrantType = "client_credentials",
                ClientId = clientId,
                ClientSecret = secret
            };
            var response = API.Post<OAuthResponse>(
                url: $"{_ignitionSettings.BaseUrl}{_ignitionSettings.OAuthUrl}",
                logger: _logger,
                name: "RequestMiddleware",
                content: request,
                apiKey: _ignitionSettings.ApiKey);
            context.Request.Headers.Add("Authorization", $"{response.TokenType} {response.AccessToken}");
            context.Request.Headers.Add("x-api-key", _ignitionSettings.ApiKey);
            return _next(context);
        }
    }
}
