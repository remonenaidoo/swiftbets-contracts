using System.Text.Json.Serialization;

namespace SwiftBets.Contracts.Messaging;

/// <summary>
/// Every event on the bus. <see cref="Context"/> is always written from 1.0.0; it reads as null only on events written
/// before, which consumers treat as <see cref="EventContext.Platform"/>.
/// </summary>
public sealed record EventEnvelope<TPayload>(
    Guid Id,
    string Type,
    int Version,
    DateTimeOffset OccurredAt,
    string CorrelationId,
    TPayload Payload,
    string? CausationId = null,
    EventContext? Context = null)
    where TPayload : IEventContract
{
    [JsonIgnore]
    public EventContext ContextOrPlatform => Context ?? EventContext.Platform;

    public static EventEnvelope<TPayload> Create(
        TPayload payload,
        DateTimeOffset occurredAt,
        string correlationId,
        string? causationId = null,
        EventContext? context = null) =>
        new(Guid.CreateVersion7(occurredAt), TPayload.EventType, TPayload.EventVersion, occurredAt, correlationId, payload, causationId, context ?? EventContext.Platform);
}
