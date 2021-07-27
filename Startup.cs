using Lombiq.AuditTrailExtensions.Services;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;

namespace OrchardCore.AuditTrail
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services) =>
            services.AddScoped<IAuditTrailContentVersionNumberService, AuditTrailContentVersionNumberService>();
    }
}
