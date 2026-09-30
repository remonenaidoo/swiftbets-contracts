namespace SwiftBets.Contracts.Messaging;

public interface IEventContract
{
    static abstract string EventType { get; }

    static abstract int EventVersion { get; }
}
