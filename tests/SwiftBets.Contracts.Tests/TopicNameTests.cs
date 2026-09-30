using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Tests;

public sealed class TopicNameTests
{
    [Fact]
    public void Topic_name_carries_prefix_version_and_environment()
    {
        var topic = TopicName.For(Topics.CouponPlaced, "dev");

        topic.Value.ShouldBe("swiftbets.placement.coupon-placed.v1.dev");
        topic.DeadLetter().Value.ShouldBe("swiftbets.placement.coupon-placed.v1.dev.dlq");
    }

    [Theory]
    [InlineData("placement.coupon-placed", "dev")]
    [InlineData("placement.coupon-placed.v1", "Dev")]
    public void Malformed_base_or_environment_is_rejected(string baseName, string environment) =>
        Should.Throw<ArgumentException>(() => TopicName.For(baseName, environment));

    [Fact]
    public void Every_declared_topic_is_well_formed() =>
        Topics.All.ShouldAllBe(t => TopicName.For(t, "ci").Value.StartsWith("swiftbets.", StringComparison.Ordinal));
}
