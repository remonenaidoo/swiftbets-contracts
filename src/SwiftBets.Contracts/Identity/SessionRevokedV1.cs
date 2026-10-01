using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Identity;

/// <summary>Sessions to end at once. A null SessionId means every session of the user (suspension, self-exclusion, closure).</summary>
public sealed record SessionRevokedV1(Guid UserId, Guid? SessionId, string Reason, DateTimeOffset RevokedAt) : IEventContract
{
    public static string EventType => "identity.session-revoked";

    public static int EventVersion => 1;
}
