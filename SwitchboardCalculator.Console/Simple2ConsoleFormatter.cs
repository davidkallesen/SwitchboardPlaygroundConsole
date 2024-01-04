namespace SwitchboardCalculator.Console;

public class Simple2ConsoleFormatter : ConsoleFormatter, IDisposable
{
    public Simple2ConsoleFormatter(IOptionsMonitor<SimpleConsoleFormatterOptions> options)
        : base("Simple2") { }

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var logLevel = logEntry.LogLevel;
        var message = logEntry.Formatter(logEntry.State, logEntry.Exception);

        // Customize the log message format here
        textWriter.WriteLine($"{logLevel}: {message}");
    }

    public void Dispose() { }
}