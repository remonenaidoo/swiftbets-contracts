using System.Text.Json;
using SwiftBets.Contracts.Identity;
using SwiftBets.Contracts.Notifications;
using SwiftBets.Contracts.Serialization;

namespace SwiftBets.Contracts.Tests;

public sealed class IdentityContractTests
{
    [Fact]
    public void Revoking_every_session_writes_a_null_session_and_reads_back()
    {
        var revoked = new SessionRevokedV1(Guid.NewGuid(), null, "selfExcluded", DateTimeOffset.UnixEpoch);

        var json = JsonSerializer.Serialize(revoked, ContractJson.Options);

        json.ShouldContain("\"sessionId\":null");
        JsonSerializer.Deserialize<SessionRevokedV1>(json, ContractJson.Options).ShouldBe(revoked);
    }

    [Fact]
    public void Account_status_travels_as_its_name()
    {
        var changed = new AccountStatusChangedV1(Guid.NewGuid(), AccountStatus.Active, AccountStatus.SelfExcluded, "customer request", "self", DateTimeOffset.UnixEpoch);

        var json = JsonSerializer.Serialize(changed, ContractJson.Options);

        json.ShouldContain("\"status\":\"selfExcluded\"");
        JsonSerializer.Deserialize<AccountStatusChangedV1>(json, ContractJson.Options).ShouldBe(changed);
    }

    [Fact]
    public void Notification_template_data_round_trips()
    {
        var requested = new NotificationRequestedV1(Guid.NewGuid(), Guid.NewGuid(), NotificationChannel.Email, "email-verification", "a@example.com",
            new Dictionary<string, string> { ["link"] = "https://example.com/verify?t=abc" }, IsMarketing: false, DateTimeOffset.UnixEpoch);

        var read = JsonSerializer.Deserialize<NotificationRequestedV1>(JsonSerializer.Serialize(requested, ContractJson.Options), ContractJson.Options)!;

        read.Data.ShouldBe(requested.Data);
        (read.Channel, read.Template, read.IsMarketing).ShouldBe((NotificationChannel.Email, "email-verification", false));
    }

    [Fact]
    public void Status_name_the_contract_does_not_know_is_rejected() =>
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<AccountStatusChangedV1>(
            """{"userId":"0199a000-0000-7000-8000-000000000000","previousStatus":"active","status":"banned","reason":"r","changedBy":"x","changedAt":"2026-10-01T12:00:00Z"}""", ContractJson.Options));
}
