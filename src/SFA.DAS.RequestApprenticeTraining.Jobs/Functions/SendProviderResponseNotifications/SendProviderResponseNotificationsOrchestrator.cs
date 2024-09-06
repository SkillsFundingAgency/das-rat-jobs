using Google.Protobuf.WellKnownTypes;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendProviderResponseNotifications
{
    public class SendProviderResponseNotificationsOrchestrator
    {
        [FunctionName("SendProviderResponseNotificationsOrchestrator")]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            
            log.LogInformation("Orchestrator started.");

            var employerRequests = await context.CallActivityAsync<List<EmployerRequestProviderResponseNotificationEmail>>("GetEmployerRequestsForNotification", null);

            var tasks = new List<Task>();

            foreach (var request in employerRequests)
            {
                tasks.Add(context.CallActivityAsync("SendProviderResponseNotifications", request));
            }

            await Task.WhenAll(tasks);
        }

    }
}
