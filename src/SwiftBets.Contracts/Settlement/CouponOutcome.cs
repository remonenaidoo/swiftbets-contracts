namespace SwiftBets.Contracts.Settlement;

public enum CouponOutcome
{
    Won,
    Lost,
    Void,

    /// <summary>The punter took a cashout; the settlement's payout is the cashout amount and no later result changes it.</summary>
    CashedOut,
}
