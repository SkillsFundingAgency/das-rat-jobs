using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.RequestApprenticeTraining.Jobs;
using SFA.DAS.RequestApprenticeTraining.Jobs.StartupExtensions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace SFA.DAS.RequestApprenticeTraining.Jobs
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.AddConfiguration();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            RegisterServices(builder.Services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services
                .AddApplicationOptions()
                .ConfigureFromOptions(f => f.EmployerRequestApprenticeTrainingOuterApiConfiguration)
                .ConfigureFromOptions(f => f.FunctionOptions)
                .AddOuterApi();
        }
    }
}
