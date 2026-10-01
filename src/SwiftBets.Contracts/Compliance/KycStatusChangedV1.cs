using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Compliance;

/// <summary>A KYC case moved on. Provider names the verification adapter; Reason is set when a case is rejected.</summary>
public sealed record KycStatusChangedV1(
    Guid UserId, Guid CaseId, KycStatus PreviousStatus, KycStatus Status, string Provider, string? Reason, DateTimeOffset ChangedAt) : IEventContract
{
    public static string EventType => "compliance.kyc-status-changed";

    public static int EventVersion => 1;
}
