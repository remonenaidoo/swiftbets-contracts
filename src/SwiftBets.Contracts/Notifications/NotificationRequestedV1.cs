using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Notifications;

/// <summary>
/// Asks notifications to send one message. Recipient is the address for the channel (empty for in-app). Marketing
/// messages are suppressed for restricted or self-excluded customers; service messages are not.
/// </summary>
public sealed record NotificationRequestedV1(
    Guid NotificationId,
    Guid UserId,
    NotificationChannel Channel,
    string Template,
    string Recipient,
    IReadOnlyDictionary<string, string> Data,
    bool IsMarketing,
    DateTimeOffset RequestedAt) : IEventContract
{
    public static string EventType => "notifications.notification-requested";

    public static int EventVersion => 1;
}
