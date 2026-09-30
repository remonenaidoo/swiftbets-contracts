namespace SwiftBets.Contracts.Messaging;

public static class MessageHeaders
{
    public const string EventId = "event-id";
    public const string EventType = "event-type";
    public const string EventVersion = "event-version";
    public const string CorrelationId = "correlation-id";
    public const string CausationId = "causation-id";
    public const string TraceParent = "traceparent";
    public const string DeadLetterSourceTopic = "dlq-source-topic";
    public const string DeadLetterSourcePartition = "dlq-source-partition";
    public const string DeadLetterSourceOffset = "dlq-source-offset";
    public const string DeadLetterReason = "dlq-reason";
    public const string RetryStep = "retry-step";
    public const string RetryAttempt = "retry-attempt";
    public const string RetryDueAt = "retry-due-at";
}
