using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OrchardCore.AuditTrail.Constants;
using OrchardCore.AuditTrail.Controllers;
using OrchardCore.AuditTrail.Models;
using OrchardCore.ContentManagement;
using OrchardCore.Entities;
using OrchardCore.Mvc.Core.Utilities;
using static OrchardCore.AuditTrail.Providers.ContentAuditTrailEventProvider;

namespace Lombiq.AuditTrailExtensions.Models
{
    public class SavedEvent
    {
        public AuditTrailEventFork AuditTrailEventFork { get; }
        public ContentItem ContentItem { get; }
        public int VersionNumber { get; }

        public SavedEvent(AuditTrailEventFork auditTrailEvent, int versionNumber)
        {
            AuditTrailEventFork = auditTrailEvent;
            ContentItem = auditTrailEvent?.As<ContentItem>(Saved);
            VersionNumber = versionNumber;
        }

        /// <summary>
        /// Returns a link to <see cref="ContentController.Detail"/> with the right version number and event ID.
        /// </summary>
        public string GenerateContentDetailLink(LinkGenerator linkGenerator, HttpContext httpContext) =>
            linkGenerator.GetUriByAction(
                httpContext,
                nameof(ContentController.Detail),
                typeof(ContentController).ControllerName(),
                new
                {
                    area = FeatureIds.Area,
                    VersionNumber,
                    auditTrailEventId = AuditTrailEventFork?.Id,
                });
    }
}
