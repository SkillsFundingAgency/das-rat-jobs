using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;
using SFA.DAS.Http.MessageHandlers;
using SFA.DAS.Http.TokenGenerators;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.RequestApprenticeTraining;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.StartupExtensions
{
    public static class AddServiceRegistrationExtensions
    {
        public static IServiceCollection AddRequestApprenticeTrainingApi(this IServiceCollection services)
        {
            services.AddScoped<DefaultHeadersHandler>();
            services.AddScoped<LoggingMessageHandler>();

            var configuration = services
                .BuildServiceProvider()
                .GetRequiredService<RequestApprenticeTrainingApiClientConfiguration>();

            services
                .AddRestEaseClient<IRequestApprenticeTrainingApi>(configuration.ApiBaseUrl)
                .AddHttpMessageHandler<DefaultHeadersHandler>()
                .AddHttpMessageHandler(sp => new ManagedIdentityHeadersHandler(new ManagedIdentityTokenGenerator(configuration)))
                .AddHttpMessageHandler<LoggingMessageHandler>();
                
            return services;
        }
    }
}
