using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Placement;

/// <summary>A leg of a V2 coupon. A banker is in every line of every bet on the coupon.</summary>
public sealed record CouponLegV2(
    Guid LegId,
    string FixtureId,
    string MarketId,
    string SelectionId,
    decimal Odds,
    long OfferVersion,
    bool IsBanker);

/// <summary>
/// One bet on a coupon: every combination of each size in <see cref="Folds"/> taken from the non-banker legs, with the
/// bankers added to each. A Trixie on three selections is folds [2, 3]; singles are [1]; an accumulator of n is [n].
/// <see cref="Stake"/> is <see cref="UnitStake"/> times <see cref="Lines"/>.
/// </summary>
public sealed record CouponBetV2(
    Guid BetId,
    string Name,
    IReadOnlyList<int> Folds,
    int Lines,
    Money.Money UnitStake,
    Money.Money Stake,
    Money.Money PotentialPayout);

/// <summary>A placed coupon with one or more bets over its legs. Replaces <see cref="CouponPlacedV1"/>, which is dual-published until 2.0.0.</summary>
public sealed record CouponPlacedV2(
    Guid CouponId,
    Guid PunterId,
    Money.Money TotalStake,
    Money.Money PotentialPayout,
    IReadOnlyList<CouponLegV2> Legs,
    IReadOnlyList<CouponBetV2> Bets,
    DateTimeOffset PlacedAt) : IEventContract
{
    public static string EventType => "placement.coupon-placed";

    public static int EventVersion => 2;
}
