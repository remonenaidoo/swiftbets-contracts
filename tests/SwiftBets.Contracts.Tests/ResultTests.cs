using SwiftBets.Contracts.Errors;
using SwiftBets.Contracts.Results;

namespace SwiftBets.Contracts.Tests;

public sealed class ResultTests
{
    [Fact]
    public void Success_exposes_the_value()
    {
        var result = Result.Success(42);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(42);
    }

    [Fact]
    public void Failure_hides_the_value_and_keeps_the_error()
    {
        Result<int> result = Error.BusinessRule("stake_too_high", "Stake exceeds the limit.");

        result.IsFailure.ShouldBeTrue();
        result.Error!.Kind.ShouldBe(ErrorKind.BusinessRule);
        Should.Throw<InvalidOperationException>(() => result.Value);
    }
}
