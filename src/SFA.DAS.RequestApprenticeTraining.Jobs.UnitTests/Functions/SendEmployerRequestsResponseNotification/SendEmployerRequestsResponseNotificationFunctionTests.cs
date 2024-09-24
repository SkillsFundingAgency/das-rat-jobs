using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Jobs.UnitTests.Helpers;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification.UnitTests
{
    public class SendEmployerRequestsResponseNotificationFunctionTests
    {
        [Test]
        public async Task RunTimerTrigger_Should_Start_New_Orchestration_And_Log_Messages()
        {
            // Arrange
            var mockDurableTaskClient = new Mock<FakeDurableTaskClient>();
            var mockLogger = new Mock<ILogger<SendEmployerRequestsResponseNotificationFunction>>();
            var timerInfo = new TimerInfo();

            var function = new SendEmployerRequestsResponseNotificationFunction(mockLogger.Object);

            mockDurableTaskClient
                .Setup(x => x.ScheduleNewOrchestrationInstanceAsync(nameof(SendEmployerRequestsResponseNotificationOrchestration), CancellationToken.None))
                .ReturnsAsync("test-instance-id");

            // Act
            await function.SendEmployerRequestsResponseNotificationTimer(timerInfo, mockDurableTaskClient.Object);

            // Assert
            mockDurableTaskClient.Verify(x =>
                x.ScheduleNewOrchestrationInstanceAsync(nameof(SendEmployerRequestsResponseNotificationOrchestration), CancellationToken.None),
                Times.Once);
        }
    }
}
