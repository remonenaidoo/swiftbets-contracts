using System.Text.RegularExpressions;

namespace SwiftBets.Contracts.Messaging;

public sealed partial record TopicName
{
    private TopicName(string value) => Value = value;

    public string Value { get; }

    public static TopicName For(string baseName, string environment)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseName);
        ArgumentException.ThrowIfNullOrWhiteSpace(environment);
        if (!BasePattern().IsMatch(baseName))
        {
            throw new ArgumentException($"Topic base '{baseName}' must look like '<domain>.<event>.v<N>'.", nameof(baseName));
        }

        if (!EnvironmentPattern().IsMatch(environment))
        {
            throw new ArgumentException($"Environment '{environment}' must be lowercase letters or digits.", nameof(environment));
        }

        return new TopicName($"swiftbets.{baseName}.{environment}");
    }

    public TopicName DeadLetter() => new($"{Value}.dlq");

    public override string ToString() => Value;

    [GeneratedRegex("^[a-z]+\\.[a-z0-9-]+\\.v[0-9]+$")]
    private static partial Regex BasePattern();

    [GeneratedRegex("^[a-z0-9]+$")]
    private static partial Regex EnvironmentPattern();
}
