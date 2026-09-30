using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Settlement;

public sealed record StuckCouponV1(Guid CouponId, string Reason, int EvaluatedLegs, int LegCount, bool Repaired, DateTimeOffset DetectedAt) : IEventContract
{
    public static string EventType => "settlement.stuck-coupon";

    public static int EventVersion => 1;
}
