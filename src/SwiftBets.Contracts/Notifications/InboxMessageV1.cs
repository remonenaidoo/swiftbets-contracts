using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Notifications;

/// <summary>A message put in one customer's in-app inbox, keyed by user id; realtime pushes it to that customer only.</summary>
public sealed record InboxMessageV1(
    Guid MessageId,
    Guid UserId,
    string Category,
    string Title,
    string Body,
    DateTimeOffset CreatedAt) : IEventContract
{
    public static string EventType => "notifications.inbox-message";

    public static int EventVersion => 1;
}
