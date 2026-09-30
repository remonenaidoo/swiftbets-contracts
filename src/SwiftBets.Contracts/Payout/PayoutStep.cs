namespace SwiftBets.Contracts.Payout;

/// <summary>Named, not positional: a retried payout resumes at the step it failed on, whatever steps are added later.</summary>
public enum PayoutStep
{
    ComputeDelta,
    CreditWallet,
    RecordPayment,
}
