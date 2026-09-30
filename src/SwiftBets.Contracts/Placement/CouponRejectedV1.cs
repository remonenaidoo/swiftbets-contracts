using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Placement;

public sealed record CouponRejectedV1(Guid CouponId, Guid PunterId, string ReasonCode, DateTimeOffset RejectedAt) : IEventContract
{
    public static string EventType => "placement.coupon-rejected";

    public static int EventVersion => 1;
}
