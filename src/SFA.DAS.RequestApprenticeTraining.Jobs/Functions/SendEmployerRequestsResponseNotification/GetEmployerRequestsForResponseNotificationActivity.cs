using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification
{
    public class GetEmployerRequestsForResponseNotificationActivity
    {
        private readonly IEmployerRequestApprenticeTrainingOuterApi _api;
        private readonly ILogger<GetEmployerRequestsForResponseNotificationActivity> _logger;

        public GetEmployerRequestsForResponseNotificationActivity(
            IEmployerRequestApprenticeTrainingOuterApi api,
            ILogger<GetEmployerRequestsForResponseNotificationActivity> logger)
        {
            _api = api;
            _logger = logger;
        }

        [Function("GetEmployerRequestsForResponseNotificationActivity")]
        public async Task<List<EmployerRequestResponseEmail>> RunActivity([ActivityTrigger] string name)
        {
            _logger.LogInformation("{ActivityName} started at {DateTimeNow}", nameof(GetEmployerRequestsForResponseNotificationActivity), DateTime.Now);

            return await _api.GetEmployerRequestsForResponseNotification();
        }
    }
}
