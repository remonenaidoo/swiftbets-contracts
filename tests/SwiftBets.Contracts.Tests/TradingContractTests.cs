using System.Text.Json;
using SwiftBets.Contracts.Messaging;
using SwiftBets.Contracts.Offer;
using SwiftBets.Contracts.Serialization;
using SwiftBets.Contracts.Trading;

namespace SwiftBets.Contracts.Tests;

public sealed class TradingContractTests
{
    private static readonly DateTimeOffset At = new(2026, 10, 2, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void A_time_void_round_trips_with_its_actions_as_names()
    {
        var result = new ManualResultV1(Guid.NewGuid(), ManualResultScope.Market, ManualResultAction.TimeVoid, "f-1", "f-1-1x2", null, null, At.AddMinutes(-3), "late kick-off", Guid.NewGuid(), At);

        var json = JsonSerializer.Serialize(result, ContractJson.Options);

        json.ShouldContain("\"timeVoid\"");
        JsonSerializer.Deserialize<ManualResultV1>(json, ContractJson.Options).ShouldBe(result);
    }

    [Fact]
    public void Rejections_and_market_status_round_trip_and_every_trading_topic_is_listed()
    {
        var rejected = new ManualResultRejectedV1(Guid.NewGuid(), Guid.NewGuid(), ManualResultRejectedV1.CouponCashedOut, "Cashed out before the result.", At);
        var suspended = new MarketStatusChangedV1("f-1", "f-1-1x2", MarketStatus.Suspended, "trader", "injury check", Guid.NewGuid(), At);

        JsonSerializer.Deserialize<ManualResultRejectedV1>(JsonSerializer.Serialize(rejected, ContractJson.Options), ContractJson.Options).ShouldBe(rejected);
        JsonSerializer.Deserialize<MarketStatusChangedV1>(JsonSerializer.Serialize(suspended, ContractJson.Options), ContractJson.Options).ShouldBe(suspended);
        Topics.All.ShouldContain(Topics.ManualResult);
        Topics.All.ShouldContain(Topics.ManualResultRejected);
        Topics.All.ShouldContain(Topics.MarketStatusChanged);
    }
}
