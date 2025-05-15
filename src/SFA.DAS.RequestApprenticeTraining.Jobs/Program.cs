using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Jobs.Extensions;

namespace SFA.DAS.RequestApprenticeTraining.Jobs
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWebApplication()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddConfiguration();
                })

                .ConfigureServices((context, services) =>
                {
                    services.AddApplicationOptions();
                    services.ConfigureFromOptions(f => f.EmployerRequestApprenticeTrainingOuterApiConfiguration);

                    services.AddOuterApi();
                    services.AddOpenTelemetryRegistration(context.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]!);

                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.SetMinimumLevel(LogLevel.Information);

                    logging.AddFilter("Microsoft", LogLevel.Warning);
                    logging.AddFilter("System", LogLevel.Warning);
                    logging.AddFilter("SFA.DAS.RequestApprenticeTraining.Jobs", LogLevel.Information);
                })
                .Build();

            await host.RunAsync();
        }
    }
}
