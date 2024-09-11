using Google.Protobuf.WellKnownTypes;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Timers;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Configuration;
using SFA.DAS.Testing.AutoFixture;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification.UnitTests
{
    public class SendEmployerRequestsResponseNotificat
    {
        [Test]
        public async Task RunTimerTrigger_Should_Start_New_Orchestration_And_Log_Messages()
        {
            // Arrange
            var mockDurableClient = new Mock<IDurableOrchestrationClient>();
            var mockLogger = new Mock<ILogger>();
            var timerInfo = new TimerInfo(null, new ScheduleStatus(), true);

            var function = new SendEmployerRequestsResponseNotificationTimerTriggerFunction();

            mockDurableClient
                .Setup(x => x.StartNewAsync("SendEmployerRequestsResponseNotificationOrchestrator", null))
                .ReturnsAsync("test-instance-id");

            // Act
            await function.RunTimerTrigger(timerInfo, mockDurableClient.Object, mockLogger.Object);

            // Assert
            mockDurableClient.Verify(x =>
                x.StartNewAsync("SendEmployerRequestsResponseNotificationOrchestrator", null),
                Times.Once);
        }
    }
}
