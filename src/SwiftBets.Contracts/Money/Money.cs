namespace SwiftBets.Contracts.Money;

/// <summary>An amount in minor units (cents) of an ISO 4217 currency.</summary>
public sealed record Money(long MinorUnits, string Currency);
