using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Steward;

/// <summary>A detector opened an incident. Kind is one of stuckCoupon, walletOutage, poisonMessage, duplicateSettlement.</summary>
public sealed record IncidentRaisedV1(Guid IncidentId, string Kind, string Subject, string Summary, DateTimeOffset OpenedAt) : IEventContract
{
    public static string EventType => "steward.incident-raised";

    public static int EventVersion => 1;
}
