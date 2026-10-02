using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Trading;

/// <summary>Settlement refused a manual result for one coupon; <c>Code</c> is stable, e.g. <c>coupon_cashed_out</c>.</summary>
public sealed record ManualResultRejectedV1(
    Guid ManualResultId,
    Guid CouponId,
    string Code,
    string Message,
    DateTimeOffset RejectedAt) : IEventContract
{
    public const string CouponCashedOut = "coupon_cashed_out";

    public static string EventType => "trading.manual-result-rejected";

    public static int EventVersion => 1;
}
