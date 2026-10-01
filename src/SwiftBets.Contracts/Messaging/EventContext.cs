namespace SwiftBets.Contracts.Messaging;

/// <summary>Where an event comes from: the brand, the customer's country and the channel. Producers without a customer use <see cref="Platform"/>.</summary>
public sealed record EventContext(string Brand, string Country, string Channel)
{
    public static EventContext Platform { get; } = new("swiftbets", "ZA", "internal");
}
