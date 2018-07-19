using Microsoft.Extensions.DependencyInjection;
using PremierAppSign.Interfaces;
using PremierAppSign.Services;

namespace PremierAppSign
{
    public partial class Startup
    {
        private static void InjectionServices(IServiceCollection services)
        {
            services.AddSingleton<IIgnitionService, IgnitionService>();
        }
    }
}
