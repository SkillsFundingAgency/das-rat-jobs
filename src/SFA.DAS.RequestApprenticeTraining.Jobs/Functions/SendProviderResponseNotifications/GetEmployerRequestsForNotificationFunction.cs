using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using SFA.DAS.RequestApprenticeTraining.Domain.Types;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests;
using System.Linq;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendProviderResponseNotifications
{
    public class GetEmployerRequestsForNotificationFunction
    {
        private readonly IEmployerRequestApprenticeTrainingOuterApi _api;

        public GetEmployerRequestsForNotificationFunction(IEmployerRequestApprenticeTrainingOuterApi api)
        {
            _api = api;
        }

        [FunctionName("GetEmployerRequestsForNotification")]
        public async Task<List<EmployerRequestProviderResponseNotificationEmail>> GetEmployerRequestsForNotification([ActivityTrigger] object input, ILogger log)
        {
            log.LogInformation("Fetching employer requests for notification.");

            return await _api.GetEmployerRequestsForResponseNotification();
        }
    }
}
