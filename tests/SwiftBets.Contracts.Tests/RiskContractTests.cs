using System.Text.Json;
using SwiftBets.Contracts.Messaging;
using SwiftBets.Contracts.Risk;
using SwiftBets.Contracts.Serialization;

namespace SwiftBets.Contracts.Tests;

public sealed class RiskContractTests
{
    [Fact]
    public void An_alert_round_trips_with_its_kind_by_name_and_exposure_limits_are_a_known_topic()
    {
        var alert = new RiskAlertV1(Guid.NewGuid(), RiskAlertKind.CorrelatedStake, "f-1", "home", [Guid.NewGuid()], [Guid.NewGuid()],
            new Money.Money(500_000, "ZAR"), "Three customers backed home inside five minutes.", new DateTimeOffset(2026, 10, 3, 9, 0, 0, TimeSpan.Zero));

        var json = JsonSerializer.Serialize(alert, ContractJson.Options);

        json.ShouldContain("\"kind\":\"correlatedStake\"");
        var back = JsonSerializer.Deserialize<RiskAlertV1>(json, ContractJson.Options)!;
        (back.Kind, back.FixtureId, back.TotalStake, back.RaisedAt).ShouldBe((alert.Kind, alert.FixtureId, alert.TotalStake, alert.RaisedAt));
        back.CouponIds.ShouldBe(alert.CouponIds);
        Topics.All.ShouldContain(Topics.ExposureLimits);
    }

    [Fact]
    public void An_exposure_limit_without_its_suspended_flag_is_rejected()
    {
        const string json = """{"fixtureId":"f-1","capMinorUnits":null,"reason":"cap","updatedAt":"2026-10-03T09:00:00Z"}""";

        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<ExposureLimitV1>(json, ContractJson.Options));
    }
}
