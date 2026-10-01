using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Audit;

/// <summary>
/// One audited action, written through the owning service's outbox in the same transaction as the change.
/// Before and After are JSON snapshots of the changed fields, null when not applicable.
/// </summary>
public sealed record AuditRecordedV1(
    Guid AuditId,
    string Service,
    string Actor,
    string Action,
    string SubjectType,
    string SubjectId,
    string? Before,
    string? After,
    string CorrelationId,
    DateTimeOffset OccurredAt) : IEventContract
{
    public static string EventType => "audit.audit-recorded";

    public static int EventVersion => 1;
}
