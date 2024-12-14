using Serilog;

namespace ECommerce.Microservice.SharedLibrary.Logging
{
    public static class LoggingService
    {
        public static ILogger CreateLogger(string fileName)
        {
            ILogger logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(path: $"{fileName}-.text",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: null,
                    fileSizeLimitBytes: null,
                    rollOnFileSizeLimit: false
                ).CreateLogger();

            return logger;
        }

        public static void LogException(Exception ex)
        {
            LogToFile(BuildExceptionDetails(ex));
            LogToConsole(ex.Message);
            LogToDebugger(ex.Message);
        }

        public static void LogToFile(string message) => Log.Information(message);
        public static void LogToConsole(string message) => Log.Warning(message);
        public static void LogToDebugger(string message) => Log.Debug(message);

        private static string BuildExceptionDetails(Exception ex)
        {
            var innerExceptionMessage = ex.InnerException?.Message ?? "None";
            return $@"
                Exception Occurred:
                -------------------
                Message: {ex.Message}
                Inner Exception: {innerExceptionMessage}
                Source: {ex.Source}
                Stack Trace: {ex.StackTrace}
                -------------------";
        }
    }
}
