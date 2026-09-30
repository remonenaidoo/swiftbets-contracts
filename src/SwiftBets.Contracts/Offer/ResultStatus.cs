namespace SwiftBets.Contracts.Offer;

/// <summary>Ordered by precedence: a higher value outranks a lower one at the same result version.</summary>
public enum ResultStatus
{
    Provisional = 1,
    Official = 2,
    Correction = 3,
    Void = 4,
}
