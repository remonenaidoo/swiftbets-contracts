namespace SwiftBets.Contracts.Trading;

/// <summary>What a manual result applies to: one coupon, one market, or every market of a fixture.</summary>
public enum ManualResultScope
{
    Coupon = 1,
    Market = 2,
    Fixture = 3,
}
