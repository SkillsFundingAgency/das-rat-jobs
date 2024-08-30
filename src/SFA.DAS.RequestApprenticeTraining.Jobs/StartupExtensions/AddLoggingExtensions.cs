﻿using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.StartupExtensions
{
    public static class AddLoggingExtensions
    {
        public static IFunctionsHostBuilder AddLogging(this IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging(ConfigureLogging);
            return builder;
        }

        public static void ConfigureLogging(this ILoggingBuilder logBuilder)
        {
            // all logging is filtered out by defualt
            logBuilder.AddFilter(typeof(Startup).Namespace, LogLevel.Information);
            var rootDirectory = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, ".."));
            var files = Directory.GetFiles(rootDirectory, "nlog.config", SearchOption.AllDirectories)[0];
            logBuilder.AddNLog(files);
        }
    }
}