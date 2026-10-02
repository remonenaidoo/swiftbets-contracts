using System.Text.Json;
using SwiftBets.Contracts.Compliance;
using SwiftBets.Contracts.Messaging;
using SwiftBets.Contracts.Notifications;
using SwiftBets.Contracts.Serialization;

namespace SwiftBets.Contracts.Tests;

public sealed class NotificationContractTests
{
    [Fact]
    public void A_limit_reached_event_round_trips_with_its_kind_and_period_by_name()
    {
        var reached = new LimitReachedV1(Guid.NewGuid(), LimitKind.Deposit, LimitPeriod.Day, "deposit", new Money.Money(50_000, "ZAR"), new DateTimeOffset(2026, 10, 3, 9, 0, 0, TimeSpan.Zero));

        var json = JsonSerializer.Serialize(reached, ContractJson.Options);

        json.ShouldContain("\"kind\":\"deposit\"");
        JsonSerializer.Deserialize<LimitReachedV1>(json, ContractJson.Options).ShouldBe(reached);
        Topics.All.ShouldContain(Topics.LimitReached);
        Topics.All.ShouldContain(Topics.InboxMessage);
    }

    [Fact]
    public void An_inbox_message_without_its_user_is_rejected()
    {
        const string json = """{"messageId":"0199aaaa-0000-7000-8000-000000000001","category":"bets","title":"t","body":"b","createdAt":"2026-10-03T09:00:00Z"}""";

        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<InboxMessageV1>(json, ContractJson.Options));
    }
}
