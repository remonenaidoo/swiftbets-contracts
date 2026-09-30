using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Offer;

public sealed record ResultPublishedV1(
    string FixtureId,
    int ResultVersion,
    ResultStatus Status,
    int HomeGoals,
    int AwayGoals,
    DateTimeOffset PublishedAt) : IEventContract
{
    public static string EventType => "offer.result-published";

    public static int EventVersion => 1;
}
