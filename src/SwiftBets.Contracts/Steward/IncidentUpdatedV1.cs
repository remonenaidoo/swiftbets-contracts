using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Steward;

/// <summary>An incident changed status (diagnosing, awaitingApproval, diagnosisFailed, resolved).</summary>
public sealed record IncidentUpdatedV1(Guid IncidentId, string Kind, string Subject, string Status, string? RootCause, DateTimeOffset UpdatedAt) : IEventContract
{
    public static string EventType => "steward.incident-updated";

    public static int EventVersion => 1;
}
