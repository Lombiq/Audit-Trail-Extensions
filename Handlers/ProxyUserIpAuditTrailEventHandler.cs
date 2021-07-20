using Microsoft.AspNetCore.Http;
using OrchardCore.AuditTrail.Services;
using OrchardCore.AuditTrail.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Lombiq.AuditTrailExtensions.Handlers
{
    public class ProxyUserIpAuditTrailEventHandler : AuditTrailEventHandlerBase
    {
        private readonly IHttpContextAccessor _hca;

        public ProxyUserIpAuditTrailEventHandler(IHttpContextAccessor hca) => _hca = hca;

        public override Task CreateAsync(AuditTrailCreateContext context)
        {
            if (_hca?.HttpContext?.Request?.Headers.ContainsKey("X-Forwarded-For") == true)
            {
                context.ClientIpAddress = _hca?.HttpContext.Request.Headers["X-Forwarded-For"]
                    .ToString()
                    .Split(',')
                    .FirstOrDefault()
                    ?.Trim();
            }

            return Task.CompletedTask;
        }
    }
}
