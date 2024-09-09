using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Jobs.Functions;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendProviderResponseNotifications
{
    public class SendProviderResponseNotificationsTimerTriggerFunction
    {
        //[FunctionName("SendProviderResponseNotificationsTimerTrigger")]
        //public async Task Run(
        //    [TimerTrigger("%FunctionsOptions:SendProviderResponseNotificationsTimerSchedule%")] TimerInfo timer,
        //    [DurableClient] IDurableOrchestrationClient starter,
        //    ILogger log)
        //{
        //    log.LogInformation("EmployerRequestHttpTrigger function processed a request.");

        //    string instanceId = await starter.StartNewAsync("SendProviderResponseNotificationsOrchestrator", null);

        //    log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

        //    return starter.CreateCheckStatusResponse(req, instanceId);
        //}

        [FunctionName("SendProviderResponseNotificationsHttpTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            try
            {
                
                log.LogInformation("SendProviderResponseNotificationsHttpTrigger function processed a request.");

                string instanceId = await starter.StartNewAsync("SendProviderResponseNotificationsOrchestrator", null);

                log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

                return starter.CreateCheckStatusResponse(req, instanceId);
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"SendProviderResponseNotificationsHttpTrigger has failed");
                throw;
            }

        }
    }
}
