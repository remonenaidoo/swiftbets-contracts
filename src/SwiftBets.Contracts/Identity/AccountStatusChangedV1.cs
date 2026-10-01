using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Identity;

/// <summary>An account moved between active, suspended, closed and self-excluded. ChangedBy is a user id or a service name.</summary>
public sealed record AccountStatusChangedV1(
    Guid UserId, AccountStatus PreviousStatus, AccountStatus Status, string Reason, string ChangedBy, DateTimeOffset ChangedAt) : IEventContract
{
    public static string EventType => "identity.account-status-changed";

    public static int EventVersion => 1;
}
