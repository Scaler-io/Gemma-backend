﻿using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using ILogger = Serilog.ILogger;
using Destructurama;

namespace Gemma.Infrastructure
{
    public class LoggerConfig
    {
        public static ILogger Configure(IConfiguration config)
        {

            var loggerOptions = new LoggerConfigOption();
            config.GetSection("LoggerConfigOption").Bind(loggerOptions);

            return new LoggerConfiguration()
                         .Destructure
                         .UsingAttributes()
                         .MinimumLevel.ControlledBy(new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Debug))
                         .MinimumLevel.Override(loggerOptions.OverrideSource, Serilog.Events.LogEventLevel.Warning)
                         .WriteTo.Console(outputTemplate: loggerOptions.OutputTemplate)
                         .Enrich.FromLogContext()
                         .Enrich.WithProperty(nameof(Environment.MachineName), Environment.MachineName)
                         .CreateLogger();
        }
    }
}
