using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.ExpireEmployerRequests
{
    public class ExpireEmployerRequestsFunction
    {
        private readonly ILogger<ExpireEmployerRequestsFunction> _log;
        private readonly IEmployerRequestApprenticeTrainingOuterApi _api;

        public ExpireEmployerRequestsFunction(ILogger<ExpireEmployerRequestsFunction> log, IEmployerRequestApprenticeTrainingOuterApi api)
        {
            _log = log;
            _api = api;
        }

        [FunctionName("ExpireEmployerRequestsTimer")]
        public async Task ExpireEmployerRequestsTimer(
            [TimerTrigger("%FunctionsOptions:ExpireEmployerRequestsTimerSchedule%")] TimerInfo timer)
        {
            try
            {
                await _api.ExpireEmployerRequests();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, $"ExpireEmployerRequestsTimer has failed");
                throw;
            }
        }

#if DEBUG
        [FunctionName("ExpireEmployerRequestsHttp")]
        public async Task ExpireEmployerRequestsHttp(
            [HttpTrigger(AuthorizationLevel.Function, "GET")] HttpRequest request)
        {
            try
            {
                _log.LogInformation($"ExpireEmployerRequestsHttp has started");
                await _api.ExpireEmployerRequests();
                _log.LogInformation($"ExpireEmployerRequestsHttp has finished");
            }
            catch (Exception ex)
            {
                _log.LogError(ex, $"ExpireEmployerRequestsHttp has failed");
                throw;
            }
        }
#endif
    }
}
