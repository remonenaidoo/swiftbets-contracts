using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Config;

/// <summary>
/// One operational setting, keyed by <see cref="Key"/> on a compacted topic: the latest record is the value in force.
/// Version only increases per key, so a consumer drops anything older than what it holds. Value is text; see
/// <see cref="ConfigKeys"/> for the keys services read and how each is parsed.
/// </summary>
public sealed record ConfigEntryV1(string Key, string Value, long Version, string ChangedBy, string Reason, DateTimeOffset ChangedAt) : IEventContract
{
    public static string EventType => "config.entry-changed";

    public static int EventVersion => 1;
}
