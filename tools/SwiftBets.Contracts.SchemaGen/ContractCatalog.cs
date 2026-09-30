using SwiftBets.Contracts.Errors;
using SwiftBets.Contracts.Offer;
using SwiftBets.Contracts.Placement;

namespace SwiftBets.Contracts.SchemaGen;

public static class ContractCatalog
{
    public static IReadOnlyList<(string Name, Type Type)> Contracts { get; } =
    [
        ($"{CouponPlacedV1.EventType}.v{CouponPlacedV1.EventVersion}", typeof(CouponPlacedV1)),
        ($"{ResultPublishedV1.EventType}.v{ResultPublishedV1.EventVersion}", typeof(ResultPublishedV1)),
        ("error-envelope", typeof(ErrorEnvelope)),
    ];
}
