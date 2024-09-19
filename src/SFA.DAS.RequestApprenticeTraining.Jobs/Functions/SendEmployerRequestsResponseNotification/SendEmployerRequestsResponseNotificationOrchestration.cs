using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification
{
    public static class SendEmployerRequestsResponseNotificationOrchestration
    {
        [Function("SendEmployerRequestsResponseNotificationOrchestration")]
        public static async Task RunOrchestrator([OrchestrationTrigger] TaskOrchestrationContext context)
        {
            ILogger logger = context.CreateReplaySafeLogger(typeof(SendEmployerRequestsResponseNotificationOrchestration));

            logger.LogInformation("{OrchestrationName} started", nameof(SendEmployerRequestsResponseNotificationOrchestration));

            var employerRequests = await context.CallActivityAsync<List<EmployerRequestResponseEmail>>(nameof(GetEmployerRequestsForResponseNotificationActivity), null, null);

            var tasks = new List<Task>();

            foreach (var request in employerRequests)
            {
                tasks.Add(context.CallActivityAsync(nameof(SendEmployerRequestsResponseNotificationActivity), request, null));
            }

            await Task.WhenAll(tasks);
        }
    }
}
