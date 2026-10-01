using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Identity;

/// <summary>A customer registered and passed the age gate. Carries no personal details; those stay in identity.</summary>
public sealed record UserRegisteredV1(Guid UserId, string Brand, string Country, string Currency, DateTimeOffset RegisteredAt) : IEventContract
{
    public static string EventType => "identity.user-registered";

    public static int EventVersion => 1;
}
