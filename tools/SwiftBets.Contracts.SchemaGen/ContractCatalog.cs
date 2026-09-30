using SwiftBets.Contracts.Errors;
using SwiftBets.Contracts.Offer;
using SwiftBets.Contracts.Payout;
using SwiftBets.Contracts.Placement;
using SwiftBets.Contracts.Settlement;
using SwiftBets.Contracts.Steward;

namespace SwiftBets.Contracts.SchemaGen;

public static class ContractCatalog
{
    public static IReadOnlyList<(string Name, Type Type)> Contracts { get; } =
    [
        ($"{CouponPlacedV1.EventType}.v{CouponPlacedV1.EventVersion}", typeof(CouponPlacedV1)),
        ($"{ResultPublishedV1.EventType}.v{ResultPublishedV1.EventVersion}", typeof(ResultPublishedV1)),
        ($"{CouponRejectedV1.EventType}.v{CouponRejectedV1.EventVersion}", typeof(CouponRejectedV1)),
        ($"{FixtureChangedV1.EventType}.v{FixtureChangedV1.EventVersion}", typeof(FixtureChangedV1)),
        ($"{LegEvaluatedV1.EventType}.v{LegEvaluatedV1.EventVersion}", typeof(LegEvaluatedV1)),
        ($"{CouponSettledV1.EventType}.v{CouponSettledV1.EventVersion}", typeof(CouponSettledV1)),
        ($"{StuckCouponV1.EventType}.v{StuckCouponV1.EventVersion}", typeof(StuckCouponV1)),
        ($"{PayoutAttemptV1.EventType}.v{PayoutAttemptV1.EventVersion}", typeof(PayoutAttemptV1)),
        ($"{PayoutCompletedV1.EventType}.v{PayoutCompletedV1.EventVersion}", typeof(PayoutCompletedV1)),
        ($"{IncidentRaisedV1.EventType}.v{IncidentRaisedV1.EventVersion}", typeof(IncidentRaisedV1)),
        ($"{IncidentUpdatedV1.EventType}.v{IncidentUpdatedV1.EventVersion}", typeof(IncidentUpdatedV1)),
        ($"{RemediationExecutedV1.EventType}.v{RemediationExecutedV1.EventVersion}", typeof(RemediationExecutedV1)),
        ("error-envelope", typeof(ErrorEnvelope)),
    ];
}
