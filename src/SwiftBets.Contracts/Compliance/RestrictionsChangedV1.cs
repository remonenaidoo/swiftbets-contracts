using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Compliance;

/// <summary>
/// The whole current responsible-gambling state of one account, keyed by user id on a compacted topic: the latest record is
/// the truth. Revision only increases, so a consumer drops anything older than what it holds.
/// </summary>
public sealed record RestrictionsChangedV1(
    Guid UserId,
    long Revision,
    IReadOnlyList<MoneyLimit> Limits,
    IReadOnlyList<Restriction> Restrictions,
    int? SessionLimitMinutes,
    int? RealityCheckMinutes,
    KycStatus KycStatus,
    DateTimeOffset ChangedAt) : IEventContract
{
    public static string EventType => "compliance.restrictions-changed";

    public static int EventVersion => 1;
}

/// <summary>Amount applies now. When PendingEffectiveAt has passed, PendingAmount applies instead (null: the limit is gone).</summary>
public sealed record MoneyLimit(
    LimitKind Kind, LimitPeriod Period, Money.Money Amount, Money.Money? PendingAmount, DateTimeOffset? PendingEffectiveAt);

/// <summary>Active from StartsAt until EndsAt; a null EndsAt lasts until an operator lifts it.</summary>
public sealed record Restriction(RestrictionKind Kind, DateTimeOffset StartsAt, DateTimeOffset? EndsAt, string Reason);
