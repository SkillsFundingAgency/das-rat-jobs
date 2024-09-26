using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.RefreshStandards
{
    public class RefreshStandardsFunction
    {
        private readonly ILogger<RefreshStandardsFunction> _logger;
        private readonly IEmployerRequestApprenticeTrainingOuterApi _api;

        public RefreshStandardsFunction(ILogger<RefreshStandardsFunction> log, IEmployerRequestApprenticeTrainingOuterApi api)
        {
            _logger = log;
            _api = api;
        }

        [Function(nameof(RefreshStandardsTimer))]
        public async Task RefreshStandardsTimer([TimerTrigger("%RefreshStandardsTimerSchedule%")] TimerInfo timer)
        {
            await Run(nameof(RefreshStandardsTimer));
        }

#if DEBUG
        [Function(nameof(RefreshStandardsHttp))]
        public async Task RefreshStandardsHttp(
            [HttpTrigger(AuthorizationLevel.Function, "GET")] HttpRequest request)
        {
            await Run(nameof(RefreshStandardsHttp));
        }
#endif
        private async Task Run(string functionName)
        {
            try
            {
                _logger.LogInformation("{FunctionName} has started", functionName);

                await _api.RefreshStandards();

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
