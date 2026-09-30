namespace SwiftBets.Contracts.Errors;

public sealed record FieldError(string Field, string Code, string Message);
