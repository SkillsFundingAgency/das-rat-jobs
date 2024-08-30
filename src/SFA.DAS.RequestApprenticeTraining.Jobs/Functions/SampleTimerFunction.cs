using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions
{
    public class SampleTimerFunction
    {
        private readonly ILogger<SampleTimerFunction> _log;
        private readonly IEmployerRequestApprenticeTrainingOuterApi _api;

        public SampleTimerFunction(ILogger<SampleTimerFunction> log, IEmployerRequestApprenticeTrainingOuterApi api)
        {
            _log = log;
            _api = api;
        }

        [FunctionName("SampleTimer")]
        public async Task SampleTimer(
            [TimerTrigger("%FunctionsOptions:SampleTimerSchedule%")] TimerInfo timer)
        {
            try
            {
                _log.LogInformation($"SampleTimer has started");
                var employerRequest = await _api.GetEmployerRequest(new Guid("1B02548D-2108-461A-A0B2-E9419B4E0A69"));
                _log.LogInformation($"SampleTimer has finished");
            }
            catch(Exception ex)
            {
                _log.LogError(ex, $"SampleTimer has failed");
                throw;
            }
        }

#if DEBUG
        [FunctionName("SampleTimerHttp")]
        public async Task SampleTimerHttp(
            [HttpTrigger(AuthorizationLevel.Function, "POST")] HttpRequest request)
        {
            try
            {
                _log.LogInformation($"SampleTimerHttp has started");
                _log.LogInformation($"SampleTimerHttp has finished");
            }
            catch (Exception ex)
            {
                _log.LogError(ex, $"SampleTimerHttp has failed");
                throw;
            }
        }
#endif
    }
}
