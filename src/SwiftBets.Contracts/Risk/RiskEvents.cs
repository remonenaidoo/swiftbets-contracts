using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Risk;

/// <summary>What the operator owes on one outcome if it wins: every open coupon backing it, at its full potential payout.</summary>
public sealed record OutcomeLiability(string MarketId, string SelectionId, Money.Money Stake, Money.Money Liability, int Coupons);

/// <summary>
/// One fixture's open liability after a coupon was placed or settled, keyed by fixture id. <c>Version</c> rises with every
/// change, so a consumer keeps the highest it has seen; <c>WorstCase</c> is the largest liability over the outcomes.
/// </summary>
public sealed record LiabilityChangedV1(
    string FixtureId,
    long Version,
    IReadOnlyList<OutcomeLiability> Outcomes,
    Money.Money WorstCase,
    DateTimeOffset ChangedAt) : IEventContract
{
    public static string EventType => "risk.liability-changed";

    public static int EventVersion => 1;
}

/// <summary>A betting pattern worth a trader's look.</summary>
public enum RiskAlertKind
{
    /// <summary>One customer placing the same selections again and again in a short window.</summary>
    RepeatedBet = 1,

    /// <summary>Several customers backing the same outcome heavily within minutes of each other.</summary>
    CorrelatedStake = 2,
}

/// <summary>Advisory: it refuses nothing. <c>PunterIds</c> and <c>CouponIds</c> are the bets that formed the pattern.</summary>
public sealed record RiskAlertV1(
    Guid AlertId,
    RiskAlertKind Kind,
    string FixtureId,
    string? SelectionId,
    IReadOnlyList<Guid> PunterIds,
    IReadOnlyList<Guid> CouponIds,
    Money.Money TotalStake,
    string Summary,
    DateTimeOffset RaisedAt) : IEventContract
{
    public static string EventType => "risk.risk-alert";

    public static int EventVersion => 1;
}

/// <summary>
/// The exposure rule in force for one fixture, keyed by fixture id on a compacted topic: the latest record wins.
/// Placement refuses new coupons on a <c>Suspended</c> fixture. <c>CapMinorUnits</c> is the worst-case liability that
/// suspends it, null for the service default.
/// </summary>
public sealed record ExposureLimitV1(
    string FixtureId,
    long? CapMinorUnits,
    bool Suspended,
    string Reason,
    DateTimeOffset UpdatedAt) : IEventContract
{
    public static string EventType => "risk.exposure-limit";

    public static int EventVersion => 1;
}
