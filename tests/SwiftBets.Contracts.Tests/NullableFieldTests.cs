using System.Text.Json;
using SwiftBets.Contracts.Serialization;
using SwiftBets.Contracts.Steward;

namespace SwiftBets.Contracts.Tests;

public sealed class NullableFieldTests
{
    [Fact]
    public void Null_field_is_written_and_reads_back()
    {
        var updated = new IncidentUpdatedV1(Guid.NewGuid(), "poisonMessage", "topic", "diagnosing", null, DateTimeOffset.UnixEpoch);

        var json = JsonSerializer.Serialize(updated, ContractJson.Options);

        json.ShouldContain("\"rootCause\":null");
        JsonSerializer.Deserialize<IncidentUpdatedV1>(json, ContractJson.Options).ShouldBe(updated);
    }

    [Fact]
    public void Message_that_leaves_the_field_out_is_rejected() =>
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<IncidentUpdatedV1>(
            """{"incidentId":"0199a000-0000-7000-8000-000000000000","kind":"k","subject":"s","status":"diagnosing","updatedAt":"2026-09-30T12:00:00Z"}""", ContractJson.Options));
}
