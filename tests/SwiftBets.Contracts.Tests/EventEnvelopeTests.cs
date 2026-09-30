using System.Text.Json;
using SwiftBets.Contracts.Messaging;
using SwiftBets.Contracts.Placement;
using SwiftBets.Contracts.Serialization;

namespace SwiftBets.Contracts.Tests;

public sealed class EventEnvelopeTests
{
    private static readonly DateTimeOffset Now = new(2026, 9, 30, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Envelope_round_trips_through_contract_json()
    {
        var payload = new CouponPlacedV1(
            Guid.NewGuid(),
            Guid.NewGuid(),
            BetType.Accumulator,
            new Money.Money(1_000, "ZAR"),
            3.375m,
            new Money.Money(3_375, "ZAR"),
            [new CouponLegV1(Guid.NewGuid(), "fx-1", "mk-1x2", "home", 1.5m, 7), new CouponLegV1(Guid.NewGuid(), "fx-2", "mk-1x2", "draw", 2.25m, 3)],
            Now);
        var envelope = EventEnvelope<CouponPlacedV1>.Create(payload, Now, "corr-1");

        var json = JsonSerializer.Serialize(envelope, ContractJson.Options);
        var back = JsonSerializer.Deserialize<EventEnvelope<CouponPlacedV1>>(json, ContractJson.Options)!;

        back.Type.ShouldBe("placement.coupon-placed");
        back.Version.ShouldBe(1);
        back.Payload.Legs.Count.ShouldBe(2);
        back.Payload.Stake.ShouldBe(payload.Stake);
        json.ShouldContain("\"betType\":\"accumulator\"");
    }

    [Fact]
    public void Envelope_missing_a_required_member_is_rejected()
    {
        const string json = """{"id":"0199a000-0000-7000-8000-000000000000","type":"placement.coupon-placed","version":1,"occurredAt":"2026-09-30T12:00:00Z","causationId":null}""";

        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<EventEnvelope<CouponPlacedV1>>(json, ContractJson.Options));
    }
}
