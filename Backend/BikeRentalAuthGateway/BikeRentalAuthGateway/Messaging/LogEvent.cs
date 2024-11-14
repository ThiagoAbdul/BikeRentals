namespace BikeRentalAuthGateway.Messaging;

public class LogEvent
{
    public string Level { get; init; }
    public string Message { get; init; }
    public string? TraceId { get; init; }
    public Guid? UserId { get; init; }
    public string Service { get; init; }

    public LogEvent(LogLevel logLevel, string message)
    {
        
    }

    private LogEvent()
    {
        
    }

    private LogEvent(string logLevel, string message, string? traceId, Guid? userId, string service)
    {
        Level = logLevel;
        Message = message;
        TraceId = traceId;
        UserId = userId;
        Service = service;
    }

    public static LogEvent Trace(string message, string? traceId = null, Guid? userId = null)
    {
        return new LogEvent(
            "TRACE",
            message,
            traceId,
            userId,
            "AuthGateway"
        );
    }

    public static LogEvent Warning(string message, string? traceId = null, Guid? userId = null)
    {
        return new LogEvent(
            "WARNING",
            message,
            traceId,
            userId,
            "AuthGateway"
        );
    }

    public static LogEvent Error(string message, string? traceId = null, Guid? userId = null)
    {
        return new LogEvent(
            "ERROR",
            message,
            traceId,
            userId,
            "AuthGateway"
        );
    }

    public static LogEvent Info(string message, string? traceId = null, Guid? userId = null)
    {
        return new LogEvent(
            "INFO",
            message,
            traceId,
            userId,
            "AuthGateway"
        );
    }

    public static LogEvent Debug(string message, string? traceId = null, Guid? userId = null)
    {
        return new LogEvent(
            "DEBUG",
            message,
            traceId,
            userId,
            "AuthGateway"
        );
    }

    public static LogEvent Critical(string message, string? traceId = null, Guid? userId = null)
    {
        return new LogEvent(
            "CRITICAL",
            message,
            traceId,
            userId,
            "AuthGateway"
        );
    }
}

