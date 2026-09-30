using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Placement;

public sealed record CouponPlacedV1(
    Guid CouponId,
    Guid PunterId,
    BetType BetType,
    Money.Money Stake,
    decimal TotalOdds,
    Money.Money PotentialPayout,
    IReadOnlyList<CouponLegV1> Legs,
    DateTimeOffset PlacedAt) : IEventContract
{
    public static string EventType => "placement.coupon-placed";

    public static int EventVersion => 1;
}
