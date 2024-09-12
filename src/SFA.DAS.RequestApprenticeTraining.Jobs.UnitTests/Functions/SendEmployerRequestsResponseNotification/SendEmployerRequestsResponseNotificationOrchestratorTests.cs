using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
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
            var mockContext = new Mock<IDurableOrchestrationContext>();
            var mockLogger = new Mock<ILogger>();

            mockContext.Setup(x => x.CallActivityAsync<List<EmployerRequestResponseEmail>>("GetEmployerRequestsForResponseNotification", null))
                       .ReturnsAsync(employerRequests);

            // Act
            await SendEmployerRequestsResponseNotificationOrchestrator.RunOrchestrator(mockContext.Object, mockLogger.Object);

            // Assert
            mockContext.Verify(x => x.CallActivityAsync<List<EmployerRequestResponseEmail>>("GetEmployerRequestsForResponseNotification", null), Times.Once);

            foreach (var request in employerRequests)
            {
                mockContext.Verify(x => x.CallActivityAsync("SendEmployerRequestsResponseNotification", request), Times.Once);
            }
        }
    }
}
