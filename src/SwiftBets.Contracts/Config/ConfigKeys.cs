using System.Globalization;
using System.Text.RegularExpressions;

namespace SwiftBets.Contracts.Config;

/// <summary>How placement takes bets: everything, pre-match only, or nothing.</summary>
public enum PlacementMode
{
    Open = 1,
    PreMatchOnly = 2,
    Closed = 3,
}

/// <summary>
/// The settings services read, and the one parser and validator for each, so the config service refuses what a consumer
/// could not read. A key a consumer does not find means its built-in default.
/// </summary>
public static partial class ConfigKeys
{
    /// <summary>"on" stops all placement at once; "off" (or absent) lets the mode decide.</summary>
    public const string PlacementKillSwitch = "placement.kill-switch";

    /// <summary>open, preMatchOnly or closed.</summary>
    public const string PlacementMode = "placement.mode";

    /// <summary>Seconds an in-play bet waits before acceptance, per sport, with a default.</summary>
    public static string LiveDelaySeconds(string sport) => $"placement.live-delay-seconds.{sport}";

    public const string LiveDelayDefault = "placement.live-delay-seconds.default";

    /// <summary>Largest stake per coupon, in minor units of the currency.</summary>
    public static string MaxStake(string currency) => $"limits.max-stake.{currency}";

    /// <summary>Largest potential payout per coupon, in minor units of the currency.</summary>
    public static string MaxPayout(string currency) => $"limits.max-payout.{currency}";

    /// <summary>A feature flag: "true" or "false".</summary>
    public static string Flag(string name) => $"flags.{name}";

    public static bool IsKillSwitchOn(string? value) => string.Equals(value, "on", StringComparison.Ordinal);

    public static PlacementMode ParseMode(string? value) => value switch
    {
        "preMatchOnly" => Config.PlacementMode.PreMatchOnly,
        "closed" => Config.PlacementMode.Closed,
        _ => Config.PlacementMode.Open,
    };

    public static long? ParseMinorUnits(string? value) =>
        long.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out var amount) ? amount : null;

    /// <summary>Why this value is not acceptable for this key, or null when it is.</summary>
    public static string? Validate(string key, string value)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(value);
        if (key == PlacementKillSwitch)
        {
            return value is "on" or "off" ? null : "The kill switch is on or off.";
        }

        if (key == PlacementMode)
        {
            return value is "open" or "preMatchOnly" or "closed" ? null : "The mode is open, preMatchOnly or closed.";
        }

        if (LiveDelayKey().IsMatch(key))
        {
            return int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out var seconds) && seconds <= 30 ? null : "A live delay is 0 to 30 seconds.";
        }

        if (LimitKey().IsMatch(key))
        {
            return ParseMinorUnits(value) is > 0 ? null : "A limit is a whole number of minor units above zero.";
        }

        if (FlagKey().IsMatch(key))
        {
            return value is "true" or "false" ? null : "A flag is true or false.";
        }

        return "Unknown setting.";
    }

    [GeneratedRegex("^placement\\.live-delay-seconds\\.[a-z0-9-]{1,40}$")]
    private static partial Regex LiveDelayKey();

    [GeneratedRegex("^limits\\.max-(stake|payout)\\.[A-Z]{3}$")]
    private static partial Regex LimitKey();

    [GeneratedRegex("^flags\\.[a-z0-9-]{1,60}$")]
    private static partial Regex FlagKey();
}
