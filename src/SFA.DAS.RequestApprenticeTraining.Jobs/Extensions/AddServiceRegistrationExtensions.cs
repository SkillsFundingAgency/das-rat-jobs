using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;
using SFA.DAS.Http.Configuration;
using SFA.DAS.Http.MessageHandlers;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Configuration;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Extensions
{
    public static class AddServiceRegistrationExtensions
    {
        public static IServiceCollection AddOuterApi(this IServiceCollection services)
        {
            services.AddScoped<DefaultHeadersHandler>();
            services.AddScoped<LoggingMessageHandler>();
            services.AddScoped<ApimHeadersHandler>();

            var configuration = services
                .BuildServiceProvider()
                .GetRequiredService<EmployerRequestApprenticeTrainingOuterApiConfiguration>();

            services
                .AddRestEaseClient<IEmployerRequestApprenticeTrainingOuterApi>(configuration.ApiBaseUrl)
                .AddHttpMessageHandler<DefaultHeadersHandler>()
                .AddHttpMessageHandler<LoggingMessageHandler>();

            services.AddTransient<IApimClientConfiguration>((_) => configuration);

            return services;
        }
    }
}
