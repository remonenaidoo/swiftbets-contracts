using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Steward;

/// <summary>An operator-approved remediation ran against the platform.</summary>
public sealed record RemediationExecutedV1(Guid IncidentId, Guid ActionId, string ActionType, string Target, bool Succeeded, string Outcome, string DecidedBy, DateTimeOffset ExecutedAt) : IEventContract
{
    public static string EventType => "steward.remediation-executed";

    public static int EventVersion => 1;
}
