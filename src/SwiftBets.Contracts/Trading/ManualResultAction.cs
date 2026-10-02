namespace SwiftBets.Contracts.Trading;

public enum ManualResultAction
{
    /// <summary>Settle with <c>WinningSelectionId</c> where the feed has no result.</summary>
    Settle = 1,

    /// <summary>Void every bet in scope; stakes are returned.</summary>
    Void = 2,

    /// <summary>Replace a feed result; settled coupons resettle with a new version.</summary>
    Override = 3,

    /// <summary>Void only bets placed at or after <c>VoidFrom</c>, for example after a late kick-off.</summary>
    TimeVoid = 4,
}
