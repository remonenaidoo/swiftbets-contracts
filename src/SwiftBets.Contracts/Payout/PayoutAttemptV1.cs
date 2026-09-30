using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Payout;

/// <summary>A payout travelling the retry ladder (5s, 1m, 15m) or parked on the dead-letter topic.</summary>
public sealed record PayoutAttemptV1(
    Guid CouponId,
    Guid PunterId,
    int SettlementVersion,
    Money.Money TargetPayout,
    PayoutStep Step,
    int Attempt,
    string? LastError,
    DateTimeOffset FirstAttemptAt) : IEventContract
{
    public static string EventType => "payout.payout-attempt";

    public static int EventVersion => 1;
}
