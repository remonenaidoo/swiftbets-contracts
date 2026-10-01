using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Identity;

public sealed record EmailVerifiedV1(Guid UserId, DateTimeOffset VerifiedAt) : IEventContract
{
    public static string EventType => "identity.email-verified";

    public static int EventVersion => 1;
}
