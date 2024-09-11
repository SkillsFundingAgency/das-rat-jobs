using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using System.Linq;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification
{
    public class SendEmployerRequestsResponseNotificationFunction
    {
        private readonly ILogger<SendEmployerRequestsResponseNotificationFunction> _logger;
        private readonly IEmployerRequestApprenticeTrainingOuterApi _api;
        private readonly IOptions<ApplicationConfiguration> _options;

        public SendEmployerRequestsResponseNotificationFunction(
            ILogger<SendEmployerRequestsResponseNotificationFunction> logger,
            IEmployerRequestApprenticeTrainingOuterApi api,
            IOptions<ApplicationConfiguration> options)
        {
            _logger = logger;
            _api = api;
            _options = options;
        }

        [FunctionName("SendEmployerRequestsResponseNotification")]
        public async Task SendEmployerRequestsResponseNotification(
            [ActivityTrigger] EmployerRequestResponseEmail response)
        {
            _logger.LogInformation($"SendEmployerRequestsResponseNotification executed at: {System.DateTime.Now}");

            // Construct the notification request
            var emailRequest = new SendEmployerRequestsResponseEmail
            {
                AccountId = response.AccountId,
                RequestedBy = response.RequestedBy,
                Standards = response.Standards.Select(s => new Infrastructure.Api.Requests.StandardDetails
                {
                    StandardTitle = s.StandardTitle,
                    StandardLevel = s.StandardLevel,
                }).ToList(),
                ManageRequestsLink = $"{_options.Value.EmployerRequestApprenticeshipTrainingBaseUrl}{{0}}/dashboard",
                ManageNotificationSettingsLink = $"{_options.Value.EmployerAccountsBaseUrl}settings/notifications",
            };

            await _api.SendEmployerRequestsResponseNotification(emailRequest);
        }
    }

}
