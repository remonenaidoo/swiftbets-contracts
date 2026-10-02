using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Trading;

/// <summary>
/// A trader's result, keyed by <c>ManualResultId</c> so a retry is idempotent. <c>MarketId</c> is set for Market scope,
/// <c>CouponId</c> for Coupon scope; <c>WinningSelectionId</c> for Settle and Override; <c>VoidFrom</c> for TimeVoid.
/// </summary>
public sealed record ManualResultV1(
    Guid ManualResultId,
    ManualResultScope Scope,
    ManualResultAction Action,
    string FixtureId,
    string? MarketId,
    Guid? CouponId,
    string? WinningSelectionId,
    DateTimeOffset? VoidFrom,
    string Reason,
    Guid OperatorId,
    DateTimeOffset IssuedAt) : IEventContract
{
    public static string EventType => "trading.manual-result";

    public static int EventVersion => 1;
}
