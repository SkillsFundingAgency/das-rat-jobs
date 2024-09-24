using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Configuration;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification
{
    public class SendEmployerRequestsResponseNotificationActivity
    {
        private readonly IEmployerRequestApprenticeTrainingOuterApi _api;
        private readonly ILogger<SendEmployerRequestsResponseNotificationActivity> _logger;
        private readonly IOptions<ApplicationConfiguration> _options;

        public SendEmployerRequestsResponseNotificationActivity(
            IEmployerRequestApprenticeTrainingOuterApi api,
            ILogger<SendEmployerRequestsResponseNotificationActivity> logger,
            IOptions<ApplicationConfiguration> options)
        {
            _api = api;
            _logger = logger;
            _options = options;
        }

        [Function("SendEmployerRequestsResponseNotificationActivity")]
        public async Task RunActivity([ActivityTrigger] EmployerRequestResponseEmail response)
        {
            _logger.LogInformation("{ActivityName} started at {DateTimeNow}", nameof(SendEmployerRequestsResponseNotificationActivity), DateTime.Now);

            var emailRequest = new SendEmployerRequestsResponseEmail
            {
                AccountId = response.AccountId,
                RequestedBy = response.RequestedBy,
                Standards = response.Standards.Select(s => new Infrastructure.Api.Requests.StandardDetails
                {
                    StandardTitle = s.StandardTitle,
                    StandardLevel = s.StandardLevel,
                }).ToList(),
                ManageRequestsLink = $"{_options.Value.EmployerRequestApprenticeshipTrainingBaseUrl}accounts/{{0}}/employer-requests/dashboard",
                ManageNotificationSettingsLink = $"{_options.Value.EmployerAccountsBaseUrl}settings/notifications",
            };

            await _api.SendEmployerRequestsResponseNotification(emailRequest);
        }
    }
}
