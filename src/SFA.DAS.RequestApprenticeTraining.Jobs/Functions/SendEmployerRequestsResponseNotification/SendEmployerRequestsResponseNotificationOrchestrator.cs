using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification
{
    public class SendEmployerRequestsResponseNotificationOrchestrator
    {
        [FunctionName("SendEmployerRequestsResponseNotificationOrchestrator")]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            
            log.LogInformation("Orchestrator started.");

            var employerRequests = await context.CallActivityAsync<List<EmployerRequestResponseEmail>>("GetEmployerRequestsForResponseNotification", null);

            var tasks = new List<Task>();

            foreach (var request in employerRequests)
            {
                tasks.Add(context.CallActivityAsync("SendEmployerRequestsResponseNotification", request));
            }

            await Task.WhenAll(tasks);
        }

    }
}
