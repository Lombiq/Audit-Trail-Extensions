using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OrchardCore.AuditTrail.Models;
using OrchardCore.ContentManagement;
using OrchardCore.Contents.AuditTrail.Controllers;
using OrchardCore.Contents.AuditTrail.Services;
using OrchardCore.Entities;
using OrchardCore.Mvc.Core.Utilities;

namespace Lombiq.AuditTrailExtensions.Models;

public class SavedEvent
{
    public AuditTrailEvent AuditTrailEvent { get; }
    public ContentItem ContentItem { get; }
    public int VersionNumber { get; }

    public SavedEvent(AuditTrailEvent auditTrailEvent, int versionNumber)
    {
        AuditTrailEvent = auditTrailEvent;
        ContentItem = auditTrailEvent?.As<ContentItem>(ContentAuditTrailEventConfiguration.Saved);
        VersionNumber = versionNumber;
    }

    /// <summary>
    /// Returns a link to <see cref="AuditTrailContentController.Display"/> with the right version number and event
    /// ID.
    /// </summary>
    public string GenerateContentDetailLink(LinkGenerator linkGenerator, HttpContext httpContext) =>
        linkGenerator.GetUriByAction(
            httpContext,
            nameof(AuditTrailContentController.Display),
            typeof(AuditTrailContentController).ControllerName(),
            new
            {
                area = "OrchardCore.AuditTrail",
                VersionNumber,
                auditTrailEventId = AuditTrailEvent?.EventId,
            });
}
