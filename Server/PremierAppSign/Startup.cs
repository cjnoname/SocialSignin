using Ignition.Common.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PremierAppSign.Configurations;
using PremierAppSign.Middlewares;

namespace PremierAppSign
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<OAuthSettings>(Configuration.GetSection("OAuth"));
            services.Configure<IgnitionConfigurations>(Configuration.GetSection("Ignition"));
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSingleton<ILogger>(
                new Logger(MetricType.INFO, Configuration["LoggingFormat"]) { EndPoint = Configuration["ProjectName"] }
            );
            InjectionServices(services);
            services.AddNodeServices(options => {
                options.LaunchWithDebugging = true;
                options.DebuggingPort = 9229;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMiddleware<RequestMiddleware>();
            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
