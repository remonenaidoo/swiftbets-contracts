namespace SwiftBets.Contracts.Placement;

public sealed record CouponLegV1(
    Guid LegId,
    string FixtureId,
    string MarketId,
    string SelectionId,
    decimal Odds,
    long OfferVersion);
