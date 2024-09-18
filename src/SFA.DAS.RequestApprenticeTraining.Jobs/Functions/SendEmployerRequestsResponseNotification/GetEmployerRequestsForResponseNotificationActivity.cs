using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification
{
    [DurableTask(nameof(GetEmployerRequestsForResponseNotificationActivity))]
    public class GetEmployerRequestsForResponseNotificationActivity : TaskActivity<object, List<EmployerRequestResponseEmail>>
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

        public async override Task<List<EmployerRequestResponseEmail>> RunAsync(TaskActivityContext context, object input)
        {
            _logger.LogInformation("{ActivityName} started at {DateTimeNow}", nameof(GetEmployerRequestsForResponseNotificationActivity), DateTime.Now);
            
            return await _api.GetEmployerRequestsForResponseNotification();
        }
    }
}
