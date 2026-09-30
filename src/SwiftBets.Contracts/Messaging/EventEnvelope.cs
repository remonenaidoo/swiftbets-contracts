namespace SwiftBets.Contracts.Messaging;

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
    public static EventEnvelope<TPayload> Create(
        TPayload payload,
        DateTimeOffset occurredAt,
        string correlationId,
        string? causationId = null) =>
        new(Guid.CreateVersion7(occurredAt), TPayload.EventType, TPayload.EventVersion, occurredAt, correlationId, payload, causationId);
}
