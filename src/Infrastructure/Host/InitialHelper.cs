using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Infrastructure
{
    public static class InitialHelper 
    {
        public static Serilog.ILogger CreateELKLogger(ELKLoggerSettings settings)
        {
            if (settings is null || string.IsNullOrEmpty(settings.AppName) || string.IsNullOrEmpty(settings.ElasticsearchURL))
                throw new ArgumentNullException(nameof(ELKLoggerSettings) + " must be passed");

            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", settings.AppName)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.Console().MinimumLevel.Verbose()
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(
                        new Uri(settings.ElasticsearchURL))
                    {
                        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                        AutoRegisterTemplate = true,
                        TemplateName = "serilog-events-template",
                        IndexFormat = string.Format("{0}-logs", settings.AppName.ToLower())
                    })
                .MinimumLevel.Verbose()
                .CreateLogger();
        }

        public static IConfiguration GetConfiguration(string basePath, string environment)
            => new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
    }
}