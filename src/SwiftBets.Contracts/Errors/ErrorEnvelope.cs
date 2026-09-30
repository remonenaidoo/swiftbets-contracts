namespace SwiftBets.Contracts.Errors;

/// <summary>RFC 7807 problem details plus a stable machine code and the request's correlation id.</summary>
public sealed record ErrorEnvelope(
    string Type,
    string Title,
    int Status,
    string Code,
    string CorrelationId,
    string? Detail = null,
    string? Instance = null,
    IReadOnlyList<FieldError>? Errors = null)
{
    public const string MediaType = "application/problem+json";
}
