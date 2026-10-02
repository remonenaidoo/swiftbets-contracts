using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Casino;

/// <summary>What a casino transaction did to the player's money.</summary>
public enum CasinoTransactionKind
{
    Bet = 1,
    Win = 2,

    /// <summary>The provider cancelled a bet; its stake goes back.</summary>
    Rollback = 3,

    /// <summary>A bet paid for by a free spin: no money moves.</summary>
    FreeSpinBet = 4,

    /// <summary>Transfer-wallet providers: money moved into the provider's session.</summary>
    TransferIn = 5,

    /// <summary>Transfer-wallet providers: the session's balance moved back to the wallet.</summary>
    TransferOut = 6,
}

/// <summary>
/// One provider callback, applied once: <c>ProviderTransactionId</c> is the provider's own id and the idempotency key, so
/// a duplicate callback never produces a second event. A rollback for a bet never seen is stored and still published,
/// with <c>Amount</c> zero, so reconciliation can account for it.
/// </summary>
public sealed record CasinoTransactionV1(
    Guid TransactionId,
    string ProviderId,
    string ProviderTransactionId,
    string RoundId,
    Guid PunterId,
    string GameId,
    CasinoTransactionKind Kind,
    Money.Money Amount,
    DateTimeOffset OccurredAt) : IEventContract
{
    public static string EventType => "casino.transaction";

    public static int EventVersion => 1;
}

public enum ReconciliationStatus
{
    Matched = 1,
    Drift = 2,
}

/// <summary>A provider's daily report compared with our ledger for the same business day.</summary>
public sealed record ProviderReconciliationV1(
    string ProviderId,
    DateOnly BusinessDate,
    Money.Money OurNet,
    Money.Money ProviderNet,
    Money.Money Drift,
    int MissingOnOurSide,
    int MissingOnProviderSide,
    ReconciliationStatus Status,
    DateTimeOffset ReconciledAt) : IEventContract
{
    public static string EventType => "casino.provider-reconciliation";

    public static int EventVersion => 1;
}
