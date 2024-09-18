using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification
{
    [DurableTask(nameof(SendEmployerRequestsResponseNotificationOrchestration))]
    public class SendEmployerRequestsResponseNotificationOrchestration : TaskOrchestrator<string, object>
    {
        public async override Task<object> RunAsync(TaskOrchestrationContext context, string input)
        {
            ILogger logger = context.CreateReplaySafeLogger<SendEmployerRequestsResponseNotificationOrchestration>();

            logger.LogInformation("{OrchestrationName} started", nameof(SendEmployerRequestsResponseNotificationOrchestration));

            var employerRequests = await context.CallActivityAsync<List<EmployerRequestResponseEmail>>(nameof(GetEmployerRequestsForResponseNotificationActivity), null, null);

            var tasks = new List<Task>();

            foreach (var request in employerRequests)
            {
                tasks.Add(context.CallActivityAsync(nameof(SendEmployerRequestsResponseNotificationActivity), request));
            }

            await Task.WhenAll(tasks);

            return Task.CompletedTask;
        }
    }
}
