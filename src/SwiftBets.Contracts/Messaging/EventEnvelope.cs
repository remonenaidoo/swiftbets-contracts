using System.Text.Json.Serialization;

namespace SwiftBets.Contracts.Messaging;

/// <summary>
/// Every event on the bus. <see cref="Context"/> is always written from 1.0.0; it reads as null only on events written
/// before, which consumers treat as <see cref="EventContext.Platform"/>. It is a property rather than a constructor
/// parameter so code built against 0.x keeps working.
/// </summary>
public sealed record EventEnvelope<TPayload>(
    Guid Id,
    string Type,
    int Version,
    DateTimeOffset OccurredAt,
    string CorrelationId,
    TPayload Payload,
    string? CausationId = null)
    where TPayload : IEventContract
{
    public EventContext? Context { get; init; }

    [JsonIgnore]
    public EventContext ContextOrPlatform => Context ?? EventContext.Platform;

    public static EventEnvelope<TPayload> Create(
        TPayload payload,
        DateTimeOffset occurredAt,
        string correlationId,
        string? causationId = null) =>
        Create(payload, occurredAt, correlationId, causationId, null);

    public static EventEnvelope<TPayload> Create(
        TPayload payload,
        DateTimeOffset occurredAt,
        string correlationId,
        string? causationId,
        EventContext? context) =>
        new(Guid.CreateVersion7(occurredAt), TPayload.EventType, TPayload.EventVersion, occurredAt, correlationId, payload, causationId)
        {
            Context = context ?? EventContext.Platform,
        };
}
