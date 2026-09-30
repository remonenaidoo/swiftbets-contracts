namespace SwiftBets.Contracts.Errors;

public sealed record Error(string Code, string Message, ErrorKind Kind)
{
    public static Error Validation(string code, string message) => new(code, message, ErrorKind.Validation);

    public static Error NotFound(string code, string message) => new(code, message, ErrorKind.NotFound);

    public static Error Conflict(string code, string message) => new(code, message, ErrorKind.Conflict);

    public static Error BusinessRule(string code, string message) => new(code, message, ErrorKind.BusinessRule);

    public static Error Unavailable(string code, string message) => new(code, message, ErrorKind.Unavailable);
}
