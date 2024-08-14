using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Configuration;
using System;
using System.IO;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.StartupExtensions
{
    internal static class AddConfigurationExtensions
    {
        internal static void AddConfiguration(this IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true);

            var preConfig = builder.ConfigurationBuilder.Build();

            if (!preConfig?["EnvironmentName"]?.Equals("DEV", StringComparison.CurrentCultureIgnoreCase) ?? false)
            {
                builder.ConfigurationBuilder.AddAzureTableStorage(options =>
                {
                    options.ConfigurationKeys = preConfig?["ConfigNames"]?.Split(",");
                    options.StorageConnectionString = preConfig?["ConfigurationStorageConnectionString"];
                    options.EnvironmentName = preConfig?["EnvironmentName"];
                    options.PreFixConfigurationKeys = false;
                });
            }
        }

        public static IServiceCollection AddApplicationOptions(this IServiceCollection services)
        {
            services
                .AddOptions<ApplicationConfiguration>()
                .Configure<IConfiguration>((settings, configuration) =>
                    configuration.Bind(settings));

            services.AddSingleton(s => s.GetRequiredService<IOptions<ApplicationConfiguration>>().Value);

            return services;
        }

        public static IServiceCollection ConfigureFromOptions<TOptions>(this IServiceCollection services, Func<ApplicationConfiguration, TOptions> func)
            where TOptions : class, new()
        {
            services.AddSingleton(s =>
                func(s.GetRequiredService<IOptions<ApplicationConfiguration>>().Value));

            return services;
        }
    }
}