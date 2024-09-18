using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using SFA.DAS.RequestApprenticeTraining.Jobs.UnitTests.Helpers;
using SFA.DAS.Testing.AutoFixture;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification.UnitTests
{
    public class SendEmployerRequestsResponseNotificationsOrchestratorTests
    {
        [Test, MoqAutoData]
        public async Task Orchestrator_Should_Call_GetEmployerRequestsAndSendNotifications(List<EmployerRequestResponseEmail> employerRequests)
        {
            // Arrange
            var mockContext = new Mock<FakeTaskOrchestrationContext>();
            var sut = new SendEmployerRequestsResponseNotificationOrchestration();

            mockContext
                .Setup(x => x.CallActivityAsync<List<EmployerRequestResponseEmail>>(nameof(GetEmployerRequestsForResponseNotificationActivity), null, null))
                .ReturnsAsync(employerRequests);

            mockContext
                .Setup(x => x.CreateReplaySafeLogger<SendEmployerRequestsResponseNotificationOrchestration>())
                .Returns(new Mock<ILogger>().Object);

            // Act
            await sut.RunAsync(mockContext.Object, string.Empty);

            // Assert
            mockContext.Verify(x => x.CallActivityAsync<List<EmployerRequestResponseEmail>>(nameof(GetEmployerRequestsForResponseNotificationActivity), null, null), Times.Once);

            foreach (var request in employerRequests)
            {
                mockContext.Verify(x => x.CallActivityAsync(nameof(SendEmployerRequestsResponseNotificationActivity), request, null), Times.Once);
            }
        }
    }
}
