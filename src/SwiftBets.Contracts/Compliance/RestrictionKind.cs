namespace SwiftBets.Contracts.Compliance;

/// <summary>A block on part or all of an account. CoolingOff and SelfExclusion block everything, including marketing.</summary>
public enum RestrictionKind
{
    CoolingOff,
    SelfExclusion,
    NoDeposits,
    NoBetting,
    NoWithdrawals,
    NoMarketing,
}
