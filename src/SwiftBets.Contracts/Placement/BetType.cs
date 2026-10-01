namespace SwiftBets.Contracts.Placement;

public enum BetType
{
    Single,
    Accumulator,

    /// <summary>More than one line, or bankers: only <see cref="CouponPlacedV2"/> can describe it.</summary>
    System,
}
