using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFA.DAS.RequestApprenticeTraining.Jobs.Extensions;

namespace SFA.DAS.RequestApprenticeTraining.Jobs
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWebApplication()
                .ConfigureServices(services =>
                {
                    services.AddApplicationInsightsTelemetryWorkerService();
                    services.ConfigureFunctionsApplicationInsights();
                    services.AddOptions();
                    services.AddApplicationOptions();
                    services.ConfigureFromOptions(f => f.EmployerRequestApprenticeTrainingOuterApiConfiguration);
                    services.ConfigureFromOptions(f => f.FunctionOptions);
                    services.AddOuterApi();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddLogging();
                })
                .Build();

                await host.RunAsync();
        }
    }
}
