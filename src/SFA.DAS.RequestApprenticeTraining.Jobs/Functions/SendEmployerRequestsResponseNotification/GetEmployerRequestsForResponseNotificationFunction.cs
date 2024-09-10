using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification
{
    public class GetEmployerRequestsForResponseNotificationFunction
    {
        private readonly IEmployerRequestApprenticeTrainingOuterApi _api;

        public GetEmployerRequestsForResponseNotificationFunction(IEmployerRequestApprenticeTrainingOuterApi api)
        {
            _api = api;
        }

        [FunctionName("GetEmployerRequestsForResponseNotification")]
        public async Task<List<EmployerRequestResponseEmail>> GetEmployerRequestsForResponseNotification([ActivityTrigger] object input, ILogger log)
        {
            log.LogInformation("Fetching employer requests for response notification.");

            return await _api.GetEmployerRequestsForResponseNotification();
        }
    }
}
