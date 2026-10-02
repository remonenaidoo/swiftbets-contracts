using System.Text.Json;
using SwiftBets.Contracts.Casino;
using SwiftBets.Contracts.Messaging;
using SwiftBets.Contracts.Serialization;

namespace SwiftBets.Contracts.Tests;

public sealed class CasinoContractTests
{
    [Fact]
    public void A_casino_transaction_round_trips_with_its_kind_by_name()
    {
        var tx = new CasinoTransactionV1(Guid.NewGuid(), "sim-seamless", "p-1", "r-1", Guid.NewGuid(), "sun-temple", CasinoTransactionKind.FreeSpinBet,
            new Money.Money(0, "ZAR"), new DateTimeOffset(2026, 10, 3, 12, 0, 0, TimeSpan.Zero));

        var json = JsonSerializer.Serialize(tx, ContractJson.Options);

        json.ShouldContain("\"kind\":\"freeSpinBet\"");
        JsonSerializer.Deserialize<CasinoTransactionV1>(json, ContractJson.Options).ShouldBe(tx);
        Topics.All.ShouldContain(Topics.CasinoTransaction);
    }

    [Fact]
    public void A_reconciliation_without_its_status_is_rejected()
    {
        const string json = """{"providerId":"sim","businessDate":"2026-10-02","ourNet":{"minorUnits":0,"currency":"ZAR"},"providerNet":{"minorUnits":0,"currency":"ZAR"},"drift":{"minorUnits":0,"currency":"ZAR"},"missingOnOurSide":0,"missingOnProviderSide":0,"reconciledAt":"2026-10-03T00:00:00Z"}""";

        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<ProviderReconciliationV1>(json, ContractJson.Options));
    }
}
