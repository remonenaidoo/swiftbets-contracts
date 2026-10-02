using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Compliance;

/// <summary>
/// The wallet refused a stake or a deposit because it would pass one of the customer's own limits. Published so the
/// customer is told; <c>Refused</c> is "stake" or "deposit".
/// </summary>
public sealed record LimitReachedV1(
    Guid UserId,
    LimitKind Kind,
    LimitPeriod Period,
    string Refused,
    Money.Money Attempted,
    DateTimeOffset ReachedAt) : IEventContract
{
    public static string EventType => "wallet.limit-reached";

    public static int EventVersion => 1;
}
