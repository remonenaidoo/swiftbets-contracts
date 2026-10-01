using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Compliance;

/// <summary>A cooling-off or self-exclusion began. Identity ends every session and notifications stops all marketing.</summary>
public sealed record SelfExclusionStartedV1(
    Guid UserId, RestrictionKind Kind, DateTimeOffset StartsAt, DateTimeOffset? EndsAt, string Reason, string ChangedBy) : IEventContract
{
    public static string EventType => "compliance.self-exclusion-started";

    public static int EventVersion => 1;
}
