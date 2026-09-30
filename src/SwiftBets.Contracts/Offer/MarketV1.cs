namespace SwiftBets.Contracts.Offer;

public sealed record MarketV1(string MarketId, MarketType Type, MarketStatus Status, IReadOnlyList<SelectionV1> Selections);
