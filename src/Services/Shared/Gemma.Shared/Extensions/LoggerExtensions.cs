﻿using Gemma.Shared.Constants;
using Serilog;
using System.Runtime.CompilerServices;

namespace Gemma.Shared.Extensions
{
    public static class LoggerExtensions
    {
        public static ILogger Here(
            this ILogger logger,
            [CallerMemberName] string member = "",
            [CallerFilePath] string caller = ""
        )
        {
            var callerType = Path.GetFileNameWithoutExtension(caller);
            return logger.ForContext(LoggerConstants.MemberName, member)
                         .ForContext(LoggerConstants.CallerType, callerType);
        }

        public static void MethodEnterd(this ILogger logger)
        {
            logger.Debug(LoggerConstants.MethodEntered);
        }

        public static void MethodExited(this ILogger logger, object withResult = null)
        {
            logger.Debug(LoggerConstants.MethodExited);
            if (withResult != null) logger.Debug("{@MethodExited} with result {@result}", LoggerConstants.MethodExited, withResult);
        }
    }
}
