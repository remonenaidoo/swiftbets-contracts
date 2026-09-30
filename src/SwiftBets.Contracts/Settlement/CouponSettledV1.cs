using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Settlement;

/// <summary>
/// Stage two: the coupon's settlement at <see cref="SettlementVersion"/>. A result correction produces a higher version;
/// payout moves only the difference between <see cref="TargetPayout"/> and what it has already paid.
/// </summary>
public sealed record CouponSettledV1(
    Guid CouponId,
    Guid PunterId,
    int SettlementVersion,
    CouponOutcome Outcome,
    Money.Money Stake,
    decimal EffectiveOdds,
    Money.Money TargetPayout,
    DateTimeOffset SettledAt) : IEventContract
{
    public static string EventType => "settlement.coupon-settled";

    public static int EventVersion => 1;
}
