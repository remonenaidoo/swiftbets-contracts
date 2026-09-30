using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Settlement;

/// <summary>Stage one of settlement: one leg judged against one version of its fixture's result. Keyed by coupon.</summary>
public sealed record LegEvaluatedV1(
    Guid CouponId,
    Guid LegId,
    string FixtureId,
    int ResultVersion,
    LegOutcome Outcome,
    DateTimeOffset EvaluatedAt) : IEventContract
{
    public static string EventType => "settlement.leg-evaluated";

    public static int EventVersion => 1;
}
