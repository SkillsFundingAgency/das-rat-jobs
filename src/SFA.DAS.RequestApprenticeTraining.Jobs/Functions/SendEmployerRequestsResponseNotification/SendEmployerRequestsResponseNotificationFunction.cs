using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using System.Linq;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification
{
    public class SendEmployerRequestsResponseNotificationFunction
    {
        private readonly ILogger<SendEmployerRequestsResponseNotificationFunction> _logger;
        private readonly IEmployerRequestApprenticeTrainingOuterApi _api;

        public SendEmployerRequestsResponseNotificationFunction(
            ILogger<SendEmployerRequestsResponseNotificationFunction> logger,
            IEmployerRequestApprenticeTrainingOuterApi api)
        {
            _logger = logger;
            _api = api;
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
                }).ToList()
            };

            await _api.SendEmployerRequestsResponseNotification(emailRequest);
        }
    }

}
