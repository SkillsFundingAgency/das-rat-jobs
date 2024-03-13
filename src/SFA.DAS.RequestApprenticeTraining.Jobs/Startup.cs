using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using SFA.DAS.NServiceBus.AzureFunction.Extensions;
using SFA.DAS.NServiceBus.Extensions;
using SFA.DAS.RequestApprenticeTraining.Domain.Configuration;
using SFA.DAS.RequestApprenticeTraining.Jobs;
using SFA.DAS.RequestApprenticeTraining.Jobs.StartupExtensions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace SFA.DAS.RequestApprenticeTraining.Jobs
{
    public class Startup : FunctionsStartup
    {
        internal const string EndpointName = "SFA.DAS.RequestApprenticeTraining";

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddConfiguration();
        }

        public override void Configure(IFunctionsHostBuilder hostBuilder)
        {
            var configuration = hostBuilder
                .AddLogging().GetContext().Configuration;

            InitialiseNServiceBus(hostBuilder, configuration);

            RegisterServices(hostBuilder.Services);
        }

        private void InitialiseNServiceBus(IFunctionsHostBuilder hostBuilder, IConfiguration configuration)
        {
            if (ServiceBusConnectionConfiguration.GetNServiceBusConnectionString(configuration) == "Disabled")
                return;

            var useManagedIdentity = ServiceBusConnectionConfiguration.DoesNServiceBusUseManagedIdentity(configuration);

            var resourceManager = new NServiceBusResourceManager(configuration, useManagedIdentity);
            resourceManager.CreateWorkAndErrorQueues(EndpointName).GetAwaiter().GetResult();
            resourceManager.SubscribeToTopicForQueue(typeof(Startup).Assembly, EndpointName).GetAwaiter().GetResult();

            hostBuilder.UseNServiceBus((IConfiguration appConfiguration) =>
            {
                var endpointConfiguration = ServiceBusEndpointFactory
                    .CreateSingleQueueConfiguration(EndpointName, appConfiguration, useManagedIdentity);

                endpointConfiguration.AdvancedConfiguration.UseNewtonsoftJsonSerializer();
                endpointConfiguration.AdvancedConfiguration.UseMessageConventions();
                endpointConfiguration.AdvancedConfiguration.EnableInstallers();
                return endpointConfiguration;
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services
                .AddApplicationOptions()
                .ConfigureFromOptions(f => f.RequestApprenticeTrainingApiConfiguration)
                .AddRequestApprenticeTrainingApi()
                .AddMediatR(typeof(Startup).Assembly);
        }
    }
}
