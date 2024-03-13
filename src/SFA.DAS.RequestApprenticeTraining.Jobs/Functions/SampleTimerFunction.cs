using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.RequestApprenticeTraining;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.RequestApprenticeTraining.Requests;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions
{
    public class SampleTimerFunction
    {
        private readonly ILogger<SampleTimerFunction> _log;
        private readonly IRequestApprenticeTrainingApi _api;

        public SampleTimerFunction(ILogger<SampleTimerFunction> log, IRequestApprenticeTrainingApi api)
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
                await _api.CreateEmployerRequest(new PostEmployerRequest { EncodedAccountId = "AAA12", RequestType = Domain.Types.RequestType.Shortlist });
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
