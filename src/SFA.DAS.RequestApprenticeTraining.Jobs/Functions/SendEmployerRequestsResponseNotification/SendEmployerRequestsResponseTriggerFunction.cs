using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification
{
    public class SendEmployerRequestsResponseNotificationTimerTriggerFunction
    {
        [FunctionName("SendEmployerRequestsResponseNotificationTimer")]
        public async Task RunTimerTrigger(
            [TimerTrigger("%FunctionsOptions:SendEmployerRequestsResponseNotificationTimerSchedule%")] TimerInfo timer,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            log.LogInformation("SendEmployerRequestsResponseNotificationTimerTrigger function processed a request.");

            string instanceId = await starter.StartNewAsync("SendEmployerRequestsResponseNotificationOrchestrator", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");
        }

        [FunctionName("SendEmployerRequestsResponseNotificationHttpTrigger")]
        public async Task<IActionResult> RunHttpTrigger(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            try
            {
                log.LogInformation("SendEmployerRequestsResponseNotificationHttpTrigger function processed a request.");

                string instanceId = await starter.StartNewAsync("SendEmployerRequestsResponseNotificationOrchestrator", null);

                log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

                return starter.CreateCheckStatusResponse(req, instanceId);
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"SendEmployerRequestsResponseNotificationHttpTrigger has failed");
                throw;
            }

        }
    }
}
