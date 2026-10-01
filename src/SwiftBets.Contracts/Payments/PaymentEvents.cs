using SwiftBets.Contracts.Messaging;

namespace SwiftBets.Contracts.Payments;

/// <summary>A provider confirmed a deposit and the wallet credited it. Keyed by user id.</summary>
public sealed record DepositSucceededV1(
    Guid PaymentId, Guid UserId, Money.Money Amount, string Provider, string ProviderReference, DateTimeOffset CompletedAt) : IEventContract
{
    public static string EventType => "payments.deposit-succeeded";

    public static int EventVersion => 1;
}

/// <summary>A deposit did not complete: the provider declined it or the wallet refused the credit (a limit or restriction).</summary>
public sealed record DepositFailedV1(
    Guid PaymentId, Guid UserId, Money.Money Amount, string Provider, string Reason, DateTimeOffset FailedAt) : IEventContract
{
    public static string EventType => "payments.deposit-failed";

    public static int EventVersion => 1;
}

/// <summary>A customer asked to withdraw; the amount is held in the wallet. RequiresApproval is set above the operator threshold.</summary>
public sealed record WithdrawalRequestedV1(
    Guid WithdrawalId, Guid UserId, Money.Money Amount, bool RequiresApproval, DateTimeOffset RequestedAt) : IEventContract
{
    public static string EventType => "payments.withdrawal-requested";

    public static int EventVersion => 1;
}

/// <summary>An operator approved or rejected a withdrawal held for approval; a rejection returns the hold.</summary>
public sealed record WithdrawalDecidedV1(
    Guid WithdrawalId, Guid UserId, bool Approved, string DecidedBy, string? Reason, DateTimeOffset DecidedAt) : IEventContract
{
    public static string EventType => "payments.withdrawal-decided";

    public static int EventVersion => 1;
}

/// <summary>The provider paid a withdrawal out and the wallet hold was captured.</summary>
public sealed record WithdrawalPaidV1(
    Guid WithdrawalId, Guid UserId, Money.Money Amount, string Provider, string ProviderReference, DateTimeOffset PaidAt) : IEventContract
{
    public static string EventType => "payments.withdrawal-paid";

    public static int EventVersion => 1;
}

/// <summary>A withdrawal ended without paying out (provider failure or rejection); the hold went back to the customer.</summary>
public sealed record WithdrawalFailedV1(
    Guid WithdrawalId, Guid UserId, Money.Money Amount, string Reason, DateTimeOffset FailedAt) : IEventContract
{
    public static string EventType => "payments.withdrawal-failed";

    public static int EventVersion => 1;
}

/// <summary>The daily provider-against-ledger reconciliation found differences for a provider and day. Keyed by provider.</summary>
public sealed record PaymentDriftDetectedV1(
    Guid RunId, string Provider, DateOnly Day, int DriftCount, Money.Money NetDifference, string Summary, DateTimeOffset DetectedAt) : IEventContract
{
    public static string EventType => "payments.drift-detected";

    public static int EventVersion => 1;
}
