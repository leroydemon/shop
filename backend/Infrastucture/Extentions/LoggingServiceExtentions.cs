
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Infrastucture.Extentions
{
    public static class LoggingServiceExtentions
    {
        public static void AddLoggingService(this IServiceCollection services)
        {
            services.RemoveAll<ILoggerProvider>();

            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "app.log");
            services.AddSingleton<ILoggerProvider>(new FileLoggerProvider(logFilePath));
        }
    }
}
