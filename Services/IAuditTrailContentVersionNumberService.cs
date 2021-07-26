using Lombiq.AuditTrailExtensions.Models;
using OrchardCore.ContentManagement;
using OrchardCore.Contents.AuditTrail.Services;
using System.Threading.Tasks;

namespace Lombiq.AuditTrailExtensions.Services
{
    /// <summary>
    /// Service for getting the version numbers of a content item or a specific version of it based on
    /// <see cref="ContentAuditTrailEventConfiguration.Saved"/> Audit Trail events.
    /// </summary>
    public interface IAuditTrailContentVersionNumberService
    {
        /// <summary>
        /// Returns the current largest version number for the content item.
        /// </summary>
        Task<int> GetLatestVersionNumberAsync(string contentItemId);

        /// <summary>
        /// Returns the version number and its content of a specific version as of the event identified by the given
        /// <paramref name="auditTrailEventId"/>.
        /// </summary>
        public Task<SavedEvent> GetCurrentVersionAsync(
            string contentItemId,
            string auditTrailEventId);
    }

    public static class ContentVersionNumberServiceExtensions
    {
        public static Task<int> GetLatestVersionNumberAsync(
            this IAuditTrailContentVersionNumberService service,
            IContent content) =>
            service.GetLatestVersionNumberAsync(content?.ContentItem?.ContentItemId);
    }
}
