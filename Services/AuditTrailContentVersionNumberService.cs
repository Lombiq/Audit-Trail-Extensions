using Finitive.AuditTrail.Indexes;
using Finitive.AuditTrail.Models;
using Lombiq.AuditTrailExtensions.Models;
using System.Threading.Tasks;
using YesSql;
using static Finitive.AuditTrail.Providers.ContentAuditTrailEventProvider;

namespace Lombiq.AuditTrailExtensions.Services
{
    public class AuditTrailContentVersionNumberService : IAuditTrailContentVersionNumberService
    {
        private readonly ISession _session;

        public AuditTrailContentVersionNumberService(ISession session) => _session = session;

        public Task<int> GetLatestVersionNumberAsync(string contentItemId) =>
            _session
                .Query<AuditTrailEventFork, ContentAuditTrailEventForkIndex>(index =>
                    index.ContentItemId == contentItemId && index.EventName == Saved)
                .CountAsync();

        public async Task<SavedEvent> GetCurrentVersionAsync(
            string contentItemId,
            string auditTrailEventId)
        {
            var auditTrailEventIndex = await _session
                .QueryIndex<AuditTrailEventIndexFork>(index => index.AuditTrailEventId == auditTrailEventId)
                .FirstOrDefaultAsync();
            if (auditTrailEventIndex == null) return null;

            var query = _session
                .Query<AuditTrailEventFork, AuditTrailEventIndexFork>(index =>
                    index.EventName == Saved && index.Id <= auditTrailEventIndex.Id)
                .With<ContentAuditTrailEventForkIndex>(index => index.ContentItemId == contentItemId)
                .OrderByDescending(index => index.Id);

            var saveEvent = await query.FirstOrDefaultAsync();
            var versionNumber = await query.CountAsync();

            return new SavedEvent(saveEvent, versionNumber);
        }
    }
}
