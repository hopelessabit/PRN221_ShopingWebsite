using Microsoft.Extensions.Logging;

public class MyLoggerProvider
{
    private readonly ILoggerFactory _loggerFactory;

    public MyLoggerProvider(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public void LogSomething()
    {
        ILogger logger = _loggerFactory.CreateLogger<MyLoggerProvider>();
        logger.LogInformation("Logging something...");
    }
}
