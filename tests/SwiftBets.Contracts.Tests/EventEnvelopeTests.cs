using System.Text.Json;
using SwiftBets.Contracts.Messaging;
using SwiftBets.Contracts.Offer;
using SwiftBets.Contracts.Serialization;

namespace SwiftBets.Contracts.Tests;

public sealed class EventEnvelopeTests
{
    private static readonly DateTimeOffset Now = new(2026, 9, 30, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Envelope_round_trips_through_contract_json()
    {
        var payload = new ResultPublishedV1("fx-1", 2, ResultStatus.Correction, 1, 2, Now);
        var envelope = EventEnvelope<ResultPublishedV1>.Create(payload, Now, "corr-1");

        var json = JsonSerializer.Serialize(envelope, ContractJson.Options);
        var back = JsonSerializer.Deserialize<EventEnvelope<ResultPublishedV1>>(json, ContractJson.Options)!;

        back.Type.ShouldBe("offer.result-published");
        back.Version.ShouldBe(1);
        back.Payload.ShouldBe(payload);
        json.ShouldContain("\"status\":\"correction\"");
    }

    [Fact]
    public void Envelope_missing_a_required_member_is_rejected()
    {
        const string json = """{"id":"0199a000-0000-7000-8000-000000000000","type":"offer.result-published","version":1,"occurredAt":"2026-09-30T12:00:00Z","causationId":null}""";

        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<EventEnvelope<ResultPublishedV1>>(json, ContractJson.Options));
    }
}
