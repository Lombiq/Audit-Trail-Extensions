using Lombiq.AuditTrailExtensions.Models;
using OrchardCore.AuditTrail.Indexes;
using OrchardCore.AuditTrail.Models;
using System.Linq;
using System.Threading.Tasks;
using YesSql;
using static OrchardCore.AuditTrail.Providers.ContentAuditTrailEventProvider;

namespace Lombiq.AuditTrailExtensions.Services
{
    public class AuditTrailContentVersionNumberService : IAuditTrailContentVersionNumberService
    {
        private readonly ISession _session;

        public AuditTrailContentVersionNumberService(ISession session) => _session = session;

        public Task<int> GetLatestVersionNumberAsync(string contentItemId) =>
            _session
                .QueryIndex<ContentAuditTrailEventIndex>(index =>
                    index.ContentItemId == contentItemId && index.EventName == Saved)
                .CountAsync();

        public async Task<SavedEvent> GetCurrentVersionAsync(
            string contentItemId,
            string auditTrailEventId)
        {
            var auditTrailEventIndex = await _session
                .QueryIndex<AuditTrailEventIndex>(index => index.AuditTrailEventId == auditTrailEventId)
                .FirstOrDefaultAsync();
            if (auditTrailEventIndex == null) return null;

            var saveEvents = (await _session
                    .Query<AuditTrailEvent, AuditTrailEventIndex>(index =>
                        index.EventName == Saved && index.Id <= auditTrailEventIndex.Id)
                    .With<ContentAuditTrailEventIndex>(index => index.ContentItemId == contentItemId)
                    .OrderByDescending(index => index.Id)
                    .ListAsync())
                .ToList();

            var saveEvent = saveEvents.FirstOrDefault();
            var versionNumber = saveEvents.Count;

            return new SavedEvent(saveEvent, versionNumber);
        }
    }
}
