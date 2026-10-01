using System.Text.Json;
using SwiftBets.Contracts.Messaging;
using SwiftBets.Contracts.Payments;
using SwiftBets.Contracts.Serialization;

namespace SwiftBets.Contracts.Tests;

public sealed class PaymentContractTests
{
    private static readonly DateTimeOffset At = new(2026, 10, 1, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Drift_report_carries_its_day_as_a_plain_date()
    {
        var drift = new PaymentDriftDetectedV1(Guid.NewGuid(), "paystack", new DateOnly(2026, 9, 30), 2, new Money.Money(-15_000, "ZAR"), "1 deposit missing from the ledger", At);

        var json = JsonSerializer.Serialize(drift, ContractJson.Options);

        json.ShouldContain("\"day\":\"2026-09-30\"");
        JsonSerializer.Deserialize<PaymentDriftDetectedV1>(json, ContractJson.Options).ShouldBe(drift);
    }

    [Fact]
    public void Withdrawal_events_round_trip_with_and_without_a_reason()
    {
        var approved = new WithdrawalDecidedV1(Guid.NewGuid(), Guid.NewGuid(), true, "ops-1", null, At);
        var failed = new WithdrawalFailedV1(Guid.NewGuid(), Guid.NewGuid(), new Money.Money(50_000, "ZAR"), "provider declined", At);

        JsonSerializer.Serialize(approved, ContractJson.Options).ShouldContain("\"reason\":null");
        JsonSerializer.Deserialize<WithdrawalDecidedV1>(JsonSerializer.Serialize(approved, ContractJson.Options), ContractJson.Options).ShouldBe(approved);
        JsonSerializer.Deserialize<WithdrawalFailedV1>(JsonSerializer.Serialize(failed, ContractJson.Options), ContractJson.Options).ShouldBe(failed);
    }

    [Fact]
    public void Payment_topics_are_registered() =>
        Topics.All.ShouldContain(Topics.PaymentDriftDetected);
}
