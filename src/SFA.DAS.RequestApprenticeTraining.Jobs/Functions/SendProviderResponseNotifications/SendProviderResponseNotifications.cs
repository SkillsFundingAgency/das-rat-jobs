using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using System.Linq;
using RestEase;
using System;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendProviderResponseNotifications
{
    public class SendProviderResponseNotificationsFunction
    {
        private readonly ILogger<SendProviderResponseNotificationsFunction> _logger;
        private readonly IEmployerRequestApprenticeTrainingOuterApi _api;

        public SendProviderResponseNotificationsFunction(
            ILogger<SendProviderResponseNotificationsFunction> logger,
            IEmployerRequestApprenticeTrainingOuterApi api)
        {
            _logger = logger;
            _api = api;
        }

        [FunctionName("SendProviderResponseNotifications")]
        public async Task SendProviderResponseNotifications(
            [ActivityTrigger] EmployerRequestProviderResponseNotificationEmail response)
        {
            _logger.LogInformation($"SendProviderResponseNotifications executed at: {System.DateTime.Now}");

            // Construct the notification request
            var notificationRequest = new SendProviderNotificationEmailRequest
            {
                AccountId = response.AccountId,
                RequestedBy = response.RequestedBy,
                Standards = response.Standards.Select(s => new Infrastructure.Api.Requests.StandardDetails
                {
                    StandardTitle = s.StandardTitle,
                    StandardLevel = s.StandardLevel,
                }).ToList()
            };

            await _api.SendProviderResponseNotifications(notificationRequest);

            _logger.LogInformation($"Email sent successfully at: {System.DateTime.Now}");
            
        }
    }

}
