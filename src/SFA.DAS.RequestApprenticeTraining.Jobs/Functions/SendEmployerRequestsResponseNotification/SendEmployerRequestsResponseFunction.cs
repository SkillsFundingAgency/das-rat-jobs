using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification
{
    public class SendEmployerRequestsResponseNotificationTriggerFunction
    {
        private readonly ILogger<SendEmployerRequestsResponseNotificationTriggerFunction> _logger;
        
        public SendEmployerRequestsResponseNotificationTriggerFunction(ILogger<SendEmployerRequestsResponseNotificationTriggerFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(SendEmployerRequestsResponseNotificationTimer))]
        public async Task SendEmployerRequestsResponseNotificationTimer([TimerTrigger("%SendEmployerRequestsResponseNotificationTimerSchedule%")] TimerInfo myTimer,
            [DurableClient] DurableTaskClient client)
        {
            await Run(nameof(SendEmployerRequestsResponseNotificationTimer), client);
        }

#if DEBUG
        [Function(nameof(SendEmployerRequestsResponseNotificationHttp))]
        public async Task SendEmployerRequestsResponseNotificationHttp(
            [HttpTrigger(AuthorizationLevel.Function, "POST")] HttpRequest request,
            [DurableClient] DurableTaskClient client)
        {
            await Run(nameof(SendEmployerRequestsResponseNotificationHttp), client);
        }
#endif

        private async Task Run(string functionName, [DurableClient] DurableTaskClient client)
        {
            try
            {
                _logger.LogInformation("{FunctionName} has been triggered", functionName);

                string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(SendEmployerRequestsResponseNotificationOrchestration), CancellationToken.None);

                _logger.LogInformation("{FunctionName} has started orchestration with {InstanceId}", functionName, instanceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{FunctionName} has has failed", functionName);
            }
        }
    }
}
