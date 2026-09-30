using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Payout;

public sealed record PayoutCompletedV1(
    Guid CouponId,
    Guid PunterId,
    int SettlementVersion,
    Money.Money Delta,
    Money.Money PaidToDate,
    DateTimeOffset CompletedAt) : IEventContract
{
    public static string EventType => "payout.payout-completed";

    public static int EventVersion => 1;
}
