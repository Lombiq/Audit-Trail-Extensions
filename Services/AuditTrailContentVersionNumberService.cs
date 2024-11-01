using Lombiq.AuditTrailExtensions.Models;
using OrchardCore.AuditTrail.Indexes;
using OrchardCore.AuditTrail.Models;
using System.Threading.Tasks;
using YesSql;
using static OrchardCore.Contents.AuditTrail.Services.ContentAuditTrailEventConfiguration;

namespace Lombiq.AuditTrailExtensions.Services;

public class AuditTrailContentVersionNumberService : IAuditTrailContentVersionNumberService
{
    private readonly ISession _session;

    public AuditTrailContentVersionNumberService(ISession session) => _session = session;

    public Task<int> GetLatestVersionNumberAsync(string contentItemId) =>
        _session
            .Query<AuditTrailEvent, AuditTrailEventIndex>(index =>
                index.CorrelationId == contentItemId && index.Name == Saved)
            .CountAsync();

    public async Task<SavedEvent> GetCurrentVersionAsync(
        string contentItemId,
        string auditTrailEventId)
    {
        var auditTrailEventIndex = await _session
            .QueryIndex<AuditTrailEventIndex>(index => index.EventId == auditTrailEventId)
            .FirstOrDefaultAsync();
        if (auditTrailEventIndex == null) return null;

        var query = _session
            .Query<AuditTrailEvent, AuditTrailEventIndex>(index =>
                index.Name == Saved && index.Id <= auditTrailEventIndex.Id)
            .With<AuditTrailEventIndex>(index => index.CorrelationId == contentItemId)
            .OrderByDescending(index => index.Id);

        var saveEvent = await query.FirstOrDefaultAsync();
        var versionNumber = await query.CountAsync();

        return new SavedEvent(saveEvent, versionNumber);
    }
}
