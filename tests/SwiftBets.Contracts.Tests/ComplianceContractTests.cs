using System.Text.Json;
using SwiftBets.Contracts.Compliance;
using SwiftBets.Contracts.Serialization;

namespace SwiftBets.Contracts.Tests;

public sealed class ComplianceContractTests
{
    private static readonly DateTimeOffset At = new(2026, 10, 1, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Restrictions_snapshot_round_trips_with_a_pending_limit_removal()
    {
        var state = new RestrictionsChangedV1(
            Guid.NewGuid(),
            7,
            [new MoneyLimit(LimitKind.Stake, LimitPeriod.Day, new Money.Money(50_000, "ZAR"), PendingAmount: null, PendingEffectiveAt: At.AddHours(24))],
            [new Restriction(RestrictionKind.NoMarketing, At, EndsAt: null, "customer request")],
            SessionLimitMinutes: 120,
            RealityCheckMinutes: 30,
            KycStatus.Pending,
            At);

        var json = JsonSerializer.Serialize(state, ContractJson.Options);
        var read = JsonSerializer.Deserialize<RestrictionsChangedV1>(json, ContractJson.Options)!;

        json.ShouldContain("\"kind\":\"stake\"");
        json.ShouldContain("\"pendingAmount\":null");
        read.Limits.ShouldBe(state.Limits);
        read.Restrictions.ShouldBe(state.Restrictions);
        (read.Revision, read.SessionLimitMinutes, read.KycStatus).ShouldBe((7L, (int?)120, KycStatus.Pending));
    }

    [Fact]
    public void Setting_a_first_limit_has_no_previous_amount()
    {
        var changed = new LimitChangedV1(Guid.NewGuid(), LimitKind.Deposit, LimitPeriod.Month, null, new Money.Money(1_000_000, "ZAR"), At, "self", At);

        var json = JsonSerializer.Serialize(changed, ContractJson.Options);

        json.ShouldContain("\"previousAmount\":null");
        JsonSerializer.Deserialize<LimitChangedV1>(json, ContractJson.Options).ShouldBe(changed);
    }

    [Fact]
    public void Self_exclusion_and_kyc_events_round_trip()
    {
        var excluded = new SelfExclusionStartedV1(Guid.NewGuid(), RestrictionKind.SelfExclusion, At, At.AddMonths(6), "customer request", "self");
        var kyc = new KycStatusChangedV1(Guid.NewGuid(), Guid.NewGuid(), KycStatus.Pending, KycStatus.Rejected, "sandbox", "document unreadable", At);

        JsonSerializer.Deserialize<SelfExclusionStartedV1>(JsonSerializer.Serialize(excluded, ContractJson.Options), ContractJson.Options).ShouldBe(excluded);
        JsonSerializer.Deserialize<KycStatusChangedV1>(JsonSerializer.Serialize(kyc, ContractJson.Options), ContractJson.Options).ShouldBe(kyc);
    }

    [Fact]
    public void Restriction_name_the_contract_does_not_know_is_rejected() =>
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<Restriction>(
            """{"kind":"banned","startsAt":"2026-10-01T12:00:00Z","endsAt":null,"reason":"r"}""", ContractJson.Options));
}
