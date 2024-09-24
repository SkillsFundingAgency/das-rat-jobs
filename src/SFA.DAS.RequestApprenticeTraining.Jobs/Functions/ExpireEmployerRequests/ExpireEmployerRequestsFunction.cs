using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.ExpireEmployerRequests
{
    public class ExpireEmployerRequestsFunction
    {
        private readonly IEmployerRequestApprenticeTrainingOuterApi _api;
        private readonly ILogger<ExpireEmployerRequestsFunction> _logger;

        public ExpireEmployerRequestsFunction(IEmployerRequestApprenticeTrainingOuterApi api, ILogger<ExpireEmployerRequestsFunction> logger)
        {
            _api = api;
            _logger = logger;
        }

        [Function(nameof(ExpireEmployerRequestsTimer))]
        public async Task ExpireEmployerRequestsTimer([TimerTrigger("%ExpireEmployerRequestsTimerSchedule%")] TimerInfo myTimer)
        {
            await Run(nameof(ExpireEmployerRequestsTimer));
        }

#if DEBUG
        [Function(nameof(ExpireEmployerRequestsHttp))]
        public async Task ExpireEmployerRequestsHttp(
            [HttpTrigger(AuthorizationLevel.Function, "GET")] HttpRequest request)
        {
            await Run(nameof(ExpireEmployerRequestsHttp));
        }
#endif

        private async Task Run(string functionName)
        {
            try
            {
                _logger.LogInformation("{FunctionName} has started", functionName);

                await _api.ExpireEmployerRequests();

                _logger.LogInformation("{FunctionName} has finished", functionName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{FunctionName} has has failed", functionName);
                throw;
            }
        }
    }
}
