using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Compliance;

/// <summary>
/// A money limit was set, lowered, raised or removed. Decreases take effect at once; increases and removals at EffectiveAt
/// after the cooling period. A null Amount means no limit.
/// </summary>
public sealed record LimitChangedV1(
    Guid UserId,
    LimitKind Kind,
    LimitPeriod Period,
    Money.Money? PreviousAmount,
    Money.Money? Amount,
    DateTimeOffset EffectiveAt,
    string ChangedBy,
    DateTimeOffset ChangedAt) : IEventContract
{
    public static string EventType => "compliance.limit-changed";

    public static int EventVersion => 1;
}
