using Microsoft.Extensions.Logging;
using NServiceBus;
using SFA.DAS.CommitmentsV2.Messages.Events;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.RequestApprenticeTraining;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Handlers.ApprovalsHandlers
{
    public class ApprenticeshipCreatedEventHandler : IHandleMessages<ApprenticeshipCreatedEvent>
    {
        private readonly IRequestApprenticeTrainingApi _api;
        private readonly ILogger<ApprenticeshipCreatedEventHandler> _logger;

        public ApprenticeshipCreatedEventHandler(
            IRequestApprenticeTrainingApi api,
            ILogger<ApprenticeshipCreatedEventHandler> logger)
        {
            _api = api;
            _logger = logger;
        }

        public async Task Handle(ApprenticeshipCreatedEvent message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Handling ApprenticeshipCreatedEvent for {message.ApprenticeshipId}");
        }
    }
}
