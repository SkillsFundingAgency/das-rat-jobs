using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.RefreshStandards
{
    public class RefreshStandardsFunction
    {
        private readonly ILogger<RefreshStandardsFunction> _log;
        private readonly IEmployerRequestApprenticeTrainingOuterApi _api;

        public RefreshStandardsFunction(ILogger<RefreshStandardsFunction> log, IEmployerRequestApprenticeTrainingOuterApi api)
        {
            _log = log;
            _api = api;
        }

        [FunctionName("RefreshStandardsFunction")]
        public async Task RefreshStandardsTimer([TimerTrigger("%FunctionsOptions:RefreshStandardsTimerSchedule%")] TimerInfo timer)
        {
            try
            {
                await _api.RefreshStandards();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, $"RefreshStandardsTimer has failed");
                throw;
            }

        }

#if DEBUG
        [FunctionName("RefreshStandardsHttp")]
        public async Task RefreshStandardsHttp(
            [HttpTrigger(AuthorizationLevel.Function, "GET")] HttpRequest request)
        {
            try
            {
                _log.LogInformation($"RefreshStandardsHttp has started");
                await _api.RefreshStandards();
                _log.LogInformation($"RefreshStandardsHttp has finished");
            }
            catch (Exception ex)
            {
                _log.LogError(ex, $"RefreshStandardsHttp has failed");
                throw;
            }
        }
#endif
    }
}
