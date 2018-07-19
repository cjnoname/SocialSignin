using Ignition.Common.Logger;
using Microsoft.Extensions.Options;
using PremierAppSign.Configurations;
using PremierAppSign.Interfaces;
using PremierAppSign.Models.Ignition;
using PremierAppSign.Utils;

namespace PremierAppSign.Services
{
    public class IgnitionService : IIgnitionService
    {
        private readonly ILogger _logger;
        private readonly IgnitionConfigurations _ignitionSettings;

        public IgnitionService(ILogger logger, IOptions<IgnitionConfigurations> ignitionSettings)
        {
            _logger = logger;
            _ignitionSettings = ignitionSettings.Value;
        }

        public object GetConfig()
        {
            return API.Get<object>(
                url: $"{_ignitionSettings.BaseUrl}{_ignitionSettings.ConfigUrl}",
                logger: _logger,
                name: "GetConfig",
                apiKey: _logger.ApiKey,
                authToken: _logger.AuthToken);
        }

        public string SignIn(SignInRequest request)
        {
            var response = API.Post<SignInResponse>(
                url: $"{_ignitionSettings.BaseUrl}{_ignitionSettings.SignInUrl}",
                logger: _logger,
                name: "SignIn",
                apiKey: _logger.ApiKey,
                authToken: _logger.AuthToken);
            return "aaa";
        }
    }
}
