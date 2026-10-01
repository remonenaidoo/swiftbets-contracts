namespace SwiftBets.Contracts.Placement;

/// <summary>
/// The lines of a bet, shared so placement prices exactly the lines settlement pays. Indexes refer to the coupon's
/// non-banker legs in order; bankers join every line.
/// </summary>
public static class SystemBets
{
    /// <summary>Named full-cover and plain bets by number of non-banker selections.</summary>
    public static IReadOnlyDictionary<string, (int Selections, int[] Folds)> Named { get; } = new Dictionary<string, (int, int[])>(StringComparer.Ordinal)
    {
        ["trixie"] = (3, [2, 3]),
        ["patent"] = (3, [1, 2, 3]),
        ["yankee"] = (4, [2, 3, 4]),
        ["lucky15"] = (4, [1, 2, 3, 4]),
        ["canadian"] = (5, [2, 3, 4, 5]),
        ["lucky31"] = (5, [1, 2, 3, 4, 5]),
        ["heinz"] = (6, [2, 3, 4, 5, 6]),
        ["lucky63"] = (6, [1, 2, 3, 4, 5, 6]),
        ["superHeinz"] = (7, [2, 3, 4, 5, 6, 7]),
        ["goliath"] = (8, [2, 3, 4, 5, 6, 7, 8]),
    };

    /// <summary>How many lines the folds make from this many non-banker selections.</summary>
    public static int Lines(int selections, IReadOnlyList<int> folds)
    {
        ArgumentNullException.ThrowIfNull(folds);
        return folds.Sum(k => checked((int)Choose(selections, k)));
    }

    /// <summary>Every line as indexes into the non-banker legs, smallest folds first, each line in ascending order.</summary>
    public static IEnumerable<int[]> Combinations(int selections, IReadOnlyList<int> folds)
    {
        ArgumentNullException.ThrowIfNull(folds);
        foreach (var k in folds.Order())
        {
            if (k < 1 || k > selections)
            {
                continue;
            }

            var line = Enumerable.Range(0, k).ToArray();
            while (true)
            {
                yield return (int[])line.Clone();
                var i = k - 1;
                while (i >= 0 && line[i] == selections - k + i)
                {
                    i--;
                }

                if (i < 0)
                {
                    break;
                }

                line[i]++;
                for (var j = i + 1; j < k; j++)
                {
                    line[j] = line[j - 1] + 1;
                }
            }
        }
    }

    /// <summary>Why these folds are not a valid bet over this many non-banker selections, or null.</summary>
    public static string? Validate(int selections, IReadOnlyList<int> folds)
    {
        ArgumentNullException.ThrowIfNull(folds);
        if (folds.Count == 0 || folds.Distinct().Count() != folds.Count)
        {
            return "A bet needs one or more distinct fold sizes.";
        }

        return folds.All(k => k >= 1 && k <= selections) ? null : $"Fold sizes run from 1 to {selections} with these selections.";
    }

    private static long Choose(int n, int k)
    {
        if (k < 0 || k > n)
        {
            return 0;
        }

        long result = 1;
        for (var i = 1; i <= k; i++)
        {
            result = result * (n - k + i) / i;
        }

        return result;
    }
}
