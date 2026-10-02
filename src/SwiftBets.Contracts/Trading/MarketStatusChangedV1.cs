using SwiftBets.Contracts.Messaging;
using SwiftBets.Contracts.Offer;

namespace SwiftBets.Contracts.Trading;

/// <summary>A market suspended, reopened or closed outside the feed. <c>Source</c> is <c>trader</c>, <c>staleness</c> or <c>config</c>.</summary>
public sealed record MarketStatusChangedV1(
    string FixtureId,
    string MarketId,
    MarketStatus Status,
    string Source,
    string Reason,
    Guid? OperatorId,
    DateTimeOffset ChangedAt) : IEventContract
{
    public static string EventType => "trading.market-status-changed";

    public static int EventVersion => 1;
}
